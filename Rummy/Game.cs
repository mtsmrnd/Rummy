using System.Runtime.InteropServices;

namespace Rummy;

public class Game
{
    public Deck Deck { get; private set; }

    //---
    private List<Player> _players = new List<Player>();
    public IReadOnlyList<Player> Players => _players;

    //---
    private Stack<Card> _discardPile = new Stack<Card>();
    public IReadOnlyCollection<Card> DiscardPile => _discardPile;
    public Card TopDiscard => _discardPile.Peek();

    //---
    private int _currentPlayerIndex = Random.Shared.Next(0, 4);
    public Player CurrentPlayer => _players[_currentPlayerIndex];
    private int _roundStartingPlayerIndex;

    //---
    private TurnPhase _currentTurnPhase;
    public TurnPhase CurrentTurnPhase => _currentTurnPhase;

    //---
    private List<Round> _rounds = new List<Round>();
    public IReadOnlyList<Round> Rounds => _rounds;
    private int _currentRoundIndex = 0;
    public Round CurrentRound => _rounds[_currentRoundIndex];
    
    //---
    public bool IsGameOver { get; private set; }
    public IReadOnlyList<Player> FinalStandings => Players.OrderBy(player => player.Points).ToList();
    
    
    public Game()
    {
        //Player creation
        Player player1 = new Player("Player 1");
        Player player2 = new Player("Player 2");
        Player player3 = new Player("Player 3");
        Player player4 = new Player("Player 4");
        _players.Add(player1);
        _players.Add(player2);
        _players.Add(player3);
        _players.Add(player4);
        //Round creation
        Round round1 = new Round(1, new List<MeldRequirement> { new MeldRequirement(MeldType.Set, 3), new MeldRequirement(MeldType.Set, 3) });
        Round round2 = new Round(2, new List<MeldRequirement> { new MeldRequirement(MeldType.Set, 3), new MeldRequirement(MeldType.Straight, 4) });
        _rounds.Add(round1);
        _rounds.Add(round2);
        //Begin
        _roundStartingPlayerIndex = _currentPlayerIndex;
        BeginRound();
    }

    private void AdvanceTurn()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        _currentTurnPhase = TurnPhase.Draw;
    }

    public void DrawFromDeck()
    {
        EnsureGameIsRunning();
        if (_currentTurnPhase != TurnPhase.Draw)
        {
            throw new InvalidOperationException("You need to be in draw phase to draw a card");
        }
        CurrentPlayer.Hand.AddCard(Deck.DrawCard());
        _currentTurnPhase = TurnPhase.Play;
        if (CurrentPlayer.Status == ObjectiveStatus.CompletedThisTurn) CurrentPlayer.ActivateObjective();
    }

    public void DrawFromDiscardPile()
    {
        EnsureGameIsRunning();
        if (_currentTurnPhase != TurnPhase.Draw)
        {
            throw new InvalidOperationException("You need to be in draw phase to draw a card");
        }
        if (_discardPile.Count < 1)
            throw new InvalidOperationException("discard pile can't be empty");
        CurrentPlayer.Hand.AddCard(_discardPile.Pop());
        _currentTurnPhase = TurnPhase.Play;
        if (CurrentPlayer.Status == ObjectiveStatus.CompletedThisTurn) CurrentPlayer.ActivateObjective();
    }

    public void EndPlayPhase()
    {
        EnsureGameIsRunning();
        if (_currentTurnPhase != TurnPhase.Play)
            throw new InvalidOperationException("You need to be in play phase to play");
        if (CheckIfWinner())
        {
            CurrentRound.SetWinner(CurrentPlayer);
            //move to ending of round
            EndRound();
            return;
        }
        _currentTurnPhase = TurnPhase.Discard;
    }

    public void DiscardFromHand(Card cardToDiscard)
    {
        EnsureGameIsRunning();
        if (_currentTurnPhase != TurnPhase.Discard)
            throw new InvalidOperationException(
                "You need to be in discard phase to discard a card"
            );
        _discardPile.Push(CurrentPlayer.Hand.RemoveCard(cardToDiscard));
        if (CheckIfWinner())
        {
            CurrentRound.SetWinner(CurrentPlayer);
            //move to ending of round
            EndRound();
            return;
        }
        AdvanceTurn();
    }

    private bool CheckIfWinner() => CurrentPlayer.Hand.Count == 0;

    private void EnsureGameIsRunning()
    {
        if (IsGameOver)
        {
            throw new InvalidOperationException("Game is Over");
        }
    }

    private void EndRound()
    {
        foreach (var player in Players)
        {
            player.AddPoints(player.Hand.GetHandValue());
        }
        if (_currentRoundIndex == _rounds.Count - 1)
        {
            IsGameOver = true;
            return;
        }

        _currentRoundIndex += 1;

        BeginRound();
    }

    private void DealHands()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].Hand.ClearHand();
        }

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < _players.Count; j++)
            {
                _players[j].Hand.AddCard(Deck.DrawCard());
            }
        }
    }

    private void BeginRound()
    {
        ResetRoundCards();
        //new starting player? Only if not first round
        if (_currentRoundIndex != 0)
        {
            _roundStartingPlayerIndex = (_roundStartingPlayerIndex + 1) % Players.Count;
            _currentPlayerIndex = _roundStartingPlayerIndex;
        }
        //new hands
        DealHands();
        //discard first card of the deck
        _discardPile.Push(Deck.DrawCard());
        //Begin game
        foreach (var player in Players)
        {
            player.ResetObjective();
        }
        _currentTurnPhase = TurnPhase.Draw;
    }

    private void ResetRoundCards()
    {
        Deck = new Deck(2);
        Deck.ShuffleDeck();

        _discardPile.Clear();
    }

    public Meld? CreateMeldIfValid(Player meldOwner, MeldType type, IEnumerable<Card> cards)
    {
        List<Card> cardsInMeld = new List<Card>(cards);
        if(!CheckCardsInHand(meldOwner, cardsInMeld)) return null;
        if (CheckDuplicateCardUse(cardsInMeld)) return null;
        //Logic check if meld is valid
        if (type == MeldType.Set)
        {
            if (cardsInMeld.Count != 3) return null;
            if (IsValidSet(cardsInMeld)) return new Meld(MeldType.Set, meldOwner, cardsInMeld);
        }
        if (type == MeldType.Straight)
        {
            if (cardsInMeld.Count == 4 || cardsInMeld.Count == 13)
            {
                if (IsValidStraight(cardsInMeld))
                {
                    return new Meld(MeldType.Straight, meldOwner, cardsInMeld);
                }
            }
        }
        return null;
    }

    private void PlayMeld(Meld meld)
    {
        foreach (Card card in meld.Cards)
        {
            CurrentPlayer.Hand.RemoveCard(card);
        }
        CurrentRound.AddMeld(meld);
    }

    private bool IsValidSet(IEnumerable<Card> cards)
    {
        List<Card> cardsInSet = new List<Card>(cards);
        if (cardsInSet.Count != 3)
        {
            return false;
        }
        int jokerCount = 0;
        Rank? expectedRank = null;

        foreach (Card card in cardsInSet)
        {
            if (card.Rank == Rank.Joker)
            {
                jokerCount++;
                continue;
            }
            if (expectedRank == null)
            {
                expectedRank = card.Rank;
            } else if (expectedRank != card.Rank)
            {
                return false;
            }
        }
        return jokerCount <= 1;
    }

    private bool IsValidStraight(IEnumerable<Card> cards)
    {
        List<Card> cardsInStraight = new List<Card>(cards);
        int cardCount = cardsInStraight.Count;
        if (cardCount != 4 && cardCount != 13) return false;
        if (!HasSameSuit(cardsInStraight)) return false;
        int jokerCount = 0;
        foreach (Card card in cardsInStraight)
        {
            if (card.Rank == Rank.Joker) jokerCount++;
        }
        if (cardCount == 4)
        {
            if (jokerCount > 1) return false;
            return IsValidStraight4(cardsInStraight);
        }
        else
        {
            return IsValidStraight13(cardsInStraight);
        }
    }
    
    private Rank NextRank(Rank rank)
    {
        return rank switch
        {
            Rank.Ace => Rank.Two,
            Rank.Two => Rank.Three,
            Rank.Three => Rank.Four,
            Rank.Four => Rank.Five,
            Rank.Five => Rank.Six,
            Rank.Six => Rank.Seven,
            Rank.Seven => Rank.Eight,
            Rank.Eight => Rank.Nine,
            Rank.Nine => Rank.Ten,
            Rank.Ten => Rank.Jack,
            Rank.Jack => Rank.Queen,
            Rank.Queen => Rank.King,
            Rank.King => Rank.Ace,
            _ => throw new ArgumentOutOfRangeException("Card Rank is invalid")
        };
    }

    private bool HasSameSuit(IEnumerable<Card> cards)
    {
        Suit? expectedSuit = null;

        foreach (Card card in cards)
        {
            if (card.Suit == Suit.Joker)
            {
                continue;
            }
            if (expectedSuit == null)
            {
                expectedSuit = card.Suit;
            }
            else if (expectedSuit != card.Suit)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsValidStraight4(IEnumerable<Card> cards)
    {
        var cardsInStraight = new List<Card>(cards);

        foreach (var startingRank in Enum.GetValues<Rank>())
        {
            if (startingRank == Rank.Joker)
                continue;

            var expectedRank = startingRank;
            var isValid = true;

            foreach (var card in cardsInStraight)
            {
                if (card.Rank != Rank.Joker && card.Rank != expectedRank)
                {
                    isValid = false;
                    break;
                }

                expectedRank = NextRank(expectedRank);
            }

            if (isValid)
                return true;
        }

        return false;
    }

    private bool IsValidStraight13(IEnumerable<Card> cards)
    {
        List<Card> cardsInStraight = new List<Card>(cards).Where(card => card.Rank != Rank.Joker).ToList();
        int jokersInStraight = cards.Count(card => card.Rank == Rank.Joker);

        Dictionary<Rank, bool> dict = new Dictionary<Rank, bool>();
        foreach (Card card in cardsInStraight)
        {
            if (dict.ContainsKey(card.Rank)) 
            {
                return false;
            }
            dict.Add(card.Rank, true);
        }
        List<Rank> ranksMissing = new List<Rank>();
        foreach (Rank rank in Enum.GetValues<Rank>())
        {
            if (rank == Rank.Joker) continue;
            if (!dict.ContainsKey(rank))
            {
                ranksMissing.Add(rank);
            }
        }
        if (ranksMissing.Count != jokersInStraight) return false;
        foreach (Rank rank in ranksMissing)
        {
            if (ranksMissing.Contains(NextRank(rank)) && rank != Rank.King)
            {
                return false;
            }
        }
        return true;
    }

    private bool ValidateMeldRequirements(IEnumerable<MeldRequirement> requirements, IEnumerable<Meld> candidates)
    {
        List<MeldRequirement> pendingRequirements = new List<MeldRequirement>(requirements);
        List<Meld> meldCandidates = new List<Meld>(candidates);

        if (pendingRequirements.Count != meldCandidates.Count) return false;

        foreach (Meld candidate in candidates)
        {
            bool candidateMatched = false;
            for (int i = 0; i < pendingRequirements.Count; i++)
            {
                if (candidate.Type == pendingRequirements[i].MeldType && candidate.Cards.Count == pendingRequirements[i].InitialSize)
                {
                    pendingRequirements.RemoveAt(i);
                    candidateMatched = true;
                    break;
                }
            }
            if (!candidateMatched) return false;
        }
        return true;    
    }

    public void TryPlayObjective(IEnumerable<Meld> candidateMelds)
    {
        EnsureGameIsRunning();
        if (CurrentTurnPhase != TurnPhase.Play) return;
        if (CurrentPlayer.Status != ObjectiveStatus.NotCompleted) return;
        if (!ValidateMeldRequirements(CurrentRound.MeldRequirements, candidateMelds)) return;

        var duplicateCheck = new HashSet<Card>();
        List<Meld> candidateMeldList = new List<Meld>(candidateMelds);
        foreach (Meld meld in candidateMeldList)
        {
            foreach (Card card in meld.Cards)
            {
                if (!duplicateCheck.Add(card)) return;
            }
            if (!CheckCardsInHand(CurrentPlayer, meld.Cards)) return;
        }
        foreach (Meld meld in candidateMeldList)
        {
            // (Add meld to round, remove cards from hand)
            PlayMeld(meld);
        }
        CurrentPlayer.CompleteObjective();
        EndPlayPhase();
    }
    
    private bool CheckDuplicateCardUse(IEnumerable<Card> cards)
    {
        var cardsInSet = new HashSet<Card>();
        foreach (Card card in cards)
        {
            if (!cardsInSet.Add(card)) return true;
        }

        return false;
    }
    
    private bool CheckCardsInHand(Player cardOwner, IEnumerable<Card> cards)
    {
        foreach (Card card in cards)
        {
            if (!cardOwner.Hand.Cards.Contains(card)) return false;
        }
        return true;
    }

    public void TryExtendMeld(Card card, Meld meld, MeldSide? side)
    {
        EnsureGameIsRunning();
        if (CurrentTurnPhase != TurnPhase.Play) return;
        if (CurrentPlayer.Status != ObjectiveStatus.Active) return;
        if (!CheckCardsInHand(CurrentPlayer, new List<Card> { card })) return;
        if (meld.Type == MeldType.Set)
        {
            if (ValidateSetExtension(card, meld))
            {
                if (meld.AddCard(card, side))
                {
                    CurrentPlayer.Hand.RemoveCard(card);
                }
            }
        }
        if (meld.Type == MeldType.Straight)
        {
            if (ValidateStraightExtension(card, meld, side))
            {
                if (meld.AddCard(card, side))
                {
                    CurrentPlayer.Hand.RemoveCard(card);
                }
            }
        }
    }

    private bool ValidateSetExtension(Card card, Meld meld) {
        if (card.Rank == Rank.Joker) return true;

        Rank? meldRank = null;

        foreach (Card cardInMeld in meld.Cards)
        {
            if (cardInMeld.Rank != Rank.Joker)
            {
                meldRank = cardInMeld.Rank;
                break;
            }
        }
        return card.Rank == meldRank;
    }

    private bool ValidateStraightExtension(Card card, Meld meld, MeldSide? side)
    {
        if (side == null) return false;
        Suit? meldSuit = null;
        foreach (Card cardInMeld in meld.Cards)
        {
            if (cardInMeld.Suit != Suit.Joker)
            {
                meldSuit = cardInMeld.Suit;
                break;
            }
        }
        if (card.Suit != meldSuit && card.Suit != Suit.Joker) return false;

        if (side == MeldSide.Left)
        {
            Rank leftRank = meld.Cards[0].Rank;
            if (card.Rank == Rank.Joker)
            {
                if (leftRank != Rank.Joker) return true;
            }
            else
            {
                if (leftRank == Rank.Joker)
                {
                    leftRank = meld.Cards[1].Rank;
                    if (NextRank(NextRank(card.Rank)) == leftRank) return true;
                }
                else
                {
                    if(NextRank(card.Rank) == leftRank) return true;
                }
            }
        }
        if (side == MeldSide.Right)
        {
            Rank rightRank = meld.Cards[meld.Cards.Count - 1].Rank;
            if (card.Rank == Rank.Joker)
            {
                if (rightRank != Rank.Joker) return true;
            }
            else
            {
                if (rightRank == Rank.Joker)
                {
                    rightRank = meld.Cards[meld.Cards.Count - 2].Rank;
                    if (NextRank(NextRank(rightRank)) == card.Rank) return true;
                }
                else
                {
                    if (NextRank(rightRank) == card.Rank) return true;
                }
            }
        }
        return false;
    }

}
