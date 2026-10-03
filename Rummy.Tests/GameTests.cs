using Rummy;

namespace Rummy.Tests;

public class GameTests
{
    [Fact]
    public void TryExtendMeld_ValidSetExtension()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Ace, Suit.Spade);
        Card card3 = new Card(Rank.Ace, Suit.Diamond);
        Meld meld = new Meld(MeldType.Set, game.CurrentPlayer, new List<Card> { card1, card2, card3 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.Ace, Suit.Club);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, null);
        
        Assert.DoesNotContain(card, game.CurrentPlayer.Hand.Cards);
        Assert.Contains(card, meld.Cards);
        Assert.Equal(initialHandCount - 1, game.CurrentPlayer.Hand.Count);
    }
    
    [Fact]
    public void TryExtendMeld_InvalidSetExtension()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Ace, Suit.Spade);
        Card card3 = new Card(Rank.Ace, Suit.Diamond);
        Meld meld = new Meld(MeldType.Set, game.CurrentPlayer, new List<Card> { card1, card2, card3 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.Two, Suit.Club);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, null);
        
        Assert.Contains(card, game.CurrentPlayer.Hand.Cards);
        Assert.DoesNotContain(card, meld.Cards);
        Assert.Equal(initialHandCount, game.CurrentPlayer.Hand.Count);
    }
    
    [Fact]
    public void TryExtendMeld_ValidStraightExtension_Right()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { card1, card2, card3, card4 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.Five, Suit.Club);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, MeldSide.Right);
        
        Assert.DoesNotContain(card, game.CurrentPlayer.Hand.Cards);
        Assert.Contains(card, meld.Cards);
        Assert.Equal(initialHandCount - 1, game.CurrentPlayer.Hand.Count);
    }
    
    [Fact]
    public void TryExtendMeld_ValidStraightExtension_Left()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { card1, card2, card3, card4 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.King, Suit.Club);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, MeldSide.Left);
        
        Assert.DoesNotContain(card, game.CurrentPlayer.Hand.Cards);
        Assert.Contains(card, meld.Cards);
        Assert.Equal(initialHandCount - 1, game.CurrentPlayer.Hand.Count);
    }
    
    [Fact]
    public void TryExtendMeld_ValidStraightExtension_Right_JokerInMeld()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Joker, Suit.Joker);
        Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { card1, card2, card3, card4 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.Five, Suit.Club);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, MeldSide.Right);
        
        Assert.DoesNotContain(card, game.CurrentPlayer.Hand.Cards);
        Assert.Contains(card, meld.Cards);
        Assert.Equal(initialHandCount - 1, game.CurrentPlayer.Hand.Count);
    }
    
    [Fact]
    public void TryExtendMeld_ValidStraightExtension_Left_JokerInMeld()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Joker, Suit.Joker);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { card1, card2, card3, card4 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.King, Suit.Club);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, MeldSide.Left);
        
        Assert.DoesNotContain(card, game.CurrentPlayer.Hand.Cards);
        Assert.Contains(card, meld.Cards);
        Assert.Equal(initialHandCount - 1, game.CurrentPlayer.Hand.Count);
    }
    
    [Fact]
    public void TryExtendMeld_InvalidStraightExtension_Left_Joker()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Joker, Suit.Joker);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { card1, card2, card3, card4 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.Joker, Suit.Joker);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, MeldSide.Left);
        
        Assert.Contains(card, game.CurrentPlayer.Hand.Cards);
        Assert.DoesNotContain(card, meld.Cards);
        Assert.Equal(initialHandCount, game.CurrentPlayer.Hand.Count);
    }
    
    [Fact]
    public void TryExtendMeld_InvalidStraightExtension_Right_Joker()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Joker, Suit.Club);
        Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { card1, card2, card3, card4 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.Joker, Suit.Joker);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, MeldSide.Right);
        
        Assert.Contains(card, game.CurrentPlayer.Hand.Cards);
        Assert.DoesNotContain(card, meld.Cards);
        Assert.Equal(initialHandCount, game.CurrentPlayer.Hand.Count);
    }
    
    [Fact]
    public void TryExtendMeld_InvalidStraightExtension_WrongSuit()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { card1, card2, card3, card4 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.Five, Suit.Diamond);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, MeldSide.Right);
        
        Assert.Contains(card, game.CurrentPlayer.Hand.Cards);
        Assert.DoesNotContain(card, meld.Cards);
        Assert.Equal(initialHandCount, game.CurrentPlayer.Hand.Count);
    }

    [Fact]
    public void TryExtendMeld_ValidStraightExtension_Right_Joker()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { card1, card2, card3, card4 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.Joker, Suit.Joker);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, MeldSide.Right);
        
        Assert.DoesNotContain(card, game.CurrentPlayer.Hand.Cards);
        Assert.Contains(card, meld.Cards);
        Assert.Equal(initialHandCount - 1, game.CurrentPlayer.Hand.Count);
    }
    [Fact]
    public void TryExtendMeld_ValidStraightExtension_Left_Joker()
    {
        //Initialize Game
        Game game = new Game();
        
        //Move to PlayPhase
        game.DrawFromDeck();

        //Create Meld
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { card1, card2, card3, card4 });
        
        //Complete Objective
        game.CurrentPlayer.CompleteObjective();
        game.CurrentPlayer.ActivateObjective();
        
        //Add card to hand to ensure valid meld add
        Card card = new Card(Rank.Joker, Suit.Joker);
        game.CurrentPlayer.Hand.AddCard(card);
        int initialHandCount = game.CurrentPlayer.Hand.Count;
        
        game.TryExtendMeld(card, meld, MeldSide.Left);
        
        Assert.DoesNotContain(card, game.CurrentPlayer.Hand.Cards);
        Assert.Contains(card, meld.Cards);
        Assert.Equal(initialHandCount - 1, game.CurrentPlayer.Hand.Count);
    }

    [Fact]
    public void DrawFromDeck_DuringDrawPhase_AddsCardAndMovesToPlayPhase()
    {
        //Initialize Game
        Game game = new Game();

        Player player = game.CurrentPlayer;
        
        int handCount = player.Hand.Count;
        int deckCount = game.Deck.Count;
        
        Assert.Equal(TurnPhase.Draw, game.CurrentTurnPhase);
        //Move to PlayPhase
        game.DrawFromDeck();
        
        Assert.Equal(deckCount - 1, game.Deck.Count);
        Assert.Equal(handCount + 1, player.Hand.Count);
        Assert.Equal(TurnPhase.Play, game.CurrentTurnPhase);
        
    }
    [Fact]
    public void DrawFromDeck_DuringPlayPhase_ThrowsException()
    {
        //Initialize Game
        Game game = new Game();

        Player player = game.CurrentPlayer;
        Assert.Equal(TurnPhase.Draw, game.CurrentTurnPhase);
        //Move to PlayPhase
        game.DrawFromDeck();
         
        int handCountBeforeIllegalDraw = player.Hand.Count;
        int deckCountBeforeIllegalDraw = game.Deck.Count;  
        TurnPhase phaseBeforeIllegalDraw = TurnPhase.Play;

        Assert.Throws<InvalidOperationException>(() => game.DrawFromDeck());
        Assert.Equal(deckCountBeforeIllegalDraw, game.Deck.Count);
        Assert.Equal(handCountBeforeIllegalDraw, player.Hand.Count);
        Assert.Equal(phaseBeforeIllegalDraw, game.CurrentTurnPhase);
    }

    [Fact]
    public void EndPlayPhase_DuringPlayPhase_MovesToDiscardPhase()
    {
        Game game = new Game();
        game.DrawFromDeck();
        game.EndPlayPhase();
        Assert.Equal(TurnPhase.Discard, game.CurrentTurnPhase);
    }
    [Fact]
    public void EndPlayPhase_WithWinner_SetsRoundWinner()
    {
        Game game = new Game();
        game.DrawFromDeck();
        Player playerExpectedWinner = game.CurrentPlayer;
        Round roundExpectedEnded = game.CurrentRound;
        playerExpectedWinner.Hand.ClearHand();
        game.EndPlayPhase();
        Assert.Equal(playerExpectedWinner, roundExpectedEnded.Winner);
    }
    [Fact]
    public void EndPlayPhase_DuringDrawPhase_ThrowsException()
    {
        Game game = new Game();
        Assert.Throws<InvalidOperationException>(() => game.EndPlayPhase());
    }
    
    [Fact]
    public void DiscardFromHand_MovesToNextPlayerDrawPhase()
    {
        Game game = new Game();
        game.DrawFromDeck();
        game.EndPlayPhase();
        Player oldPlayer = game.CurrentPlayer;
        Card cardToDiscard = oldPlayer.Hand.Cards[0];
        int oldDiscardPileCount = game.DiscardPile.Count;
        int oldHandCount = oldPlayer.Hand.Count;
        
        game.DiscardFromHand(cardToDiscard);
        
        Assert.DoesNotContain(cardToDiscard, oldPlayer.Hand.Cards);
        Assert.Equal(cardToDiscard, game.TopDiscard);
        Assert.Equal(oldDiscardPileCount + 1, game.DiscardPile.Count);
        Assert.Equal(oldHandCount - 1, oldPlayer.Hand.Count);
        Assert.Equal(TurnPhase.Draw, game.CurrentTurnPhase);
        Assert.NotEqual(oldPlayer, game.CurrentPlayer);
    }
    
    [Fact]
    public void DiscardFromHand_WithWinner_SetsRoundWinner()
    {
        Game game = new Game();
        Player expectedWinner = game.CurrentPlayer;
        Round roundExpectedEnded = game.CurrentRound;
        game.DrawFromDeck();
        game.EndPlayPhase();
        expectedWinner.Hand.ClearHand();
        expectedWinner.Hand.AddCard(new Card(Rank.Ace, Suit.Club));
        game.DiscardFromHand(game.CurrentPlayer.Hand.Cards[0]);
        Assert.Equal(expectedWinner, roundExpectedEnded.Winner);
    }
    [Fact]
    public void DiscardFromHand_DuringPlayPhase_ThrowsException()
    {
        Game game = new Game();
        game.DrawFromDeck();
        Assert.Throws<InvalidOperationException>(() => game.DiscardFromHand(game.CurrentPlayer.Hand.Cards[0]));
    }

    [Fact]
    public void DrawFromDiscardPile_DuringDrawPhase_AddsTopDiscardAndMovesToPlay()
    {
        Game game = new Game();
        game.DrawFromDeck();
        game.EndPlayPhase();
        Card cardToDiscard = game.CurrentPlayer.Hand.Cards[0];
        game.DiscardFromHand(cardToDiscard);
        int oldDiscardPileCount = game.DiscardPile.Count;
        int handCount = game.CurrentPlayer.Hand.Count;
        game.DrawFromDiscardPile();
        
        Assert.Contains(cardToDiscard, game.CurrentPlayer.Hand.Cards);
        Assert.Equal(TurnPhase.Play, game.CurrentTurnPhase);
        Assert.Equal(oldDiscardPileCount - 1, game.DiscardPile.Count);
        Assert.Equal(handCount + 1, game.CurrentPlayer.Hand.Count);
    }
    [Fact]
    public void DrawFromDiscardPile_DuringPlayPhase_ThrowsException()
    {
        Game game = new Game();
        game.DrawFromDeck();
        Assert.Throws<InvalidOperationException>(() => game.DrawFromDiscardPile());
    }
    [Fact]
    public void DrawFromDiscardPile_EmptyDiscardPile_ThrowsException()
    {
        Game game = new Game();
        game.DrawFromDeck();
        game.EndPlayPhase();
        Card cardToDiscard = game.CurrentPlayer.Hand.Cards[0];
        game.DiscardFromHand(cardToDiscard);
        game.DrawFromDiscardPile();
        Assert.Throws<InvalidOperationException>(() => game.DrawFromDiscardPile());
    }

    [Fact]
    public void TryPlayObjective_PlaysMelds_AdvancesPhase()
    {
        Game game = new Game();
        game.DrawFromDeck();
        Player playerToCheck = game.CurrentPlayer;
        Round currentRound = game.CurrentRound;
        
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Ace, Suit.Spade);
        Card card3 = new Card(Rank.Ace, Suit.Diamond);
        Card card4 = new Card(Rank.Queen, Suit.Club);
        Card card5 = new Card(Rank.Queen, Suit.Spade);
        Card card6 = new Card(Rank.Queen, Suit.Diamond);
        Card card7 = new Card(Rank.Joker, Suit.Joker);

        playerToCheck.Hand.ClearHand();
        playerToCheck.Hand.AddCard(card1);
        playerToCheck.Hand.AddCard(card2);
        playerToCheck.Hand.AddCard(card3);
        playerToCheck.Hand.AddCard(card4);
        playerToCheck.Hand.AddCard(card5);
        playerToCheck.Hand.AddCard(card6);
        playerToCheck.Hand.AddCard(card7);

        int cardsInHand = playerToCheck.Hand.Count;
        int meldsInRound = currentRound.Melds.Count;
        
        Meld candidate1 = new Meld(MeldType.Set, playerToCheck, new List<Card>{card1, card2, card3});
        Meld candidate2 = new Meld(MeldType.Set, playerToCheck, new List<Card>{card4, card5, card6});
        game.TryPlayObjective(new List<Meld>{candidate1, candidate2});

        Assert.Contains(candidate1, currentRound.Melds);
        Assert.Contains(candidate2, currentRound.Melds);

        Assert.Equal(TurnPhase.Discard, game.CurrentTurnPhase);
        Assert.Equal(cardsInHand-6, playerToCheck.Hand.Count);
        Assert.Equal(meldsInRound+2, currentRound.Melds.Count);
        Assert.Equal(card7, playerToCheck.Hand.Cards[0]);
        Assert.Equal(ObjectiveStatus.CompletedThisTurn, playerToCheck.Status);
    }

    [Fact]
    public void CreateMeldIfValid_DuplicateUse_Rejected()
    {
        Game game = new Game();
        game.CurrentPlayer.Hand.ClearHand();
        var card1 = new Card(Rank.Ace, Suit.Club);
        game.CurrentPlayer.Hand.AddCard(card1);
        
        var meld = game.CreateMeldIfValid(game.CurrentPlayer, MeldType.Set, new List<Card> { card1, card1, card1 });
        Assert.Null(meld);
    }
    [Fact]
    public void CreateMeldIfValid_SameCardDifferentObject_Accepted()
    {
        Game game = new Game();
        game.CurrentPlayer.Hand.ClearHand();
        var card1 = new Card(Rank.Ace, Suit.Club);
        var card2 = new Card(Rank.Ace, Suit.Club);
        var card3 = new Card(Rank.Ace, Suit.Club);

        game.CurrentPlayer.Hand.AddCard(card1);
        game.CurrentPlayer.Hand.AddCard(card2);
        game.CurrentPlayer.Hand.AddCard(card3);
        
        var meld = game.CreateMeldIfValid(game.CurrentPlayer, MeldType.Set, new List<Card> { card1, card2, card3 });
        Assert.NotNull(meld);
    }
    [Fact]
    public void TryPlayObjective_DuplicateInDifferentMeld_Rejected()
    {
        Game game = new Game();
        Player player = game.CurrentPlayer;
        game.DrawFromDeck();
        player.Hand.ClearHand();
        var card1 = new Card(Rank.Ace, Suit.Club);
        var card2 = new Card(Rank.Ace, Suit.Club);
        var card3 = new Card(Rank.Ace, Suit.Club);
        var card4 = new Card(Rank.Ace, Suit.Club);
        var card5 = new Card(Rank.Ace, Suit.Club);
        
        player.Hand.AddCard(card1);
        player.Hand.AddCard(card2);
        player.Hand.AddCard(card3);
        player.Hand.AddCard(card4);
        player.Hand.AddCard(card5);

        int handCount = player.Hand.Count;
        var phase = game.CurrentTurnPhase;
        int meldCount = game.CurrentRound.Melds.Count;
        var status = player.Status;

        var meld1 = game.CreateMeldIfValid(player, MeldType.Set, new List<Card> { card1, card2, card3 });
        var meld2 = game.CreateMeldIfValid(player, MeldType.Set, new List<Card> { card1, card4, card5 });

        game.TryPlayObjective(new List<Meld>{meld1, meld2});

        Assert.NotNull(meld1);
        Assert.NotNull(meld2);
        Assert.Equal(handCount, player.Hand.Count);
        Assert.Equal(phase, game.CurrentTurnPhase);
        Assert.Equal(status, player.Status);
        Assert.Equal(meldCount, game.CurrentRound.Melds.Count);

    }
    
    [Fact]
    public void CreateMeldIfValid_ForNonCurrentPlayer_ValidCards_Accepted()
    {
        Game game = new Game();
        Player player = game.CurrentPlayer;
        game.DrawFromDeck();
        game.CurrentPlayer.Hand.ClearHand();
        var card1 = new Card(Rank.Ace, Suit.Club);
        var card2 = new Card(Rank.Ace, Suit.Club);
        var card3 = new Card(Rank.Ace, Suit.Club);
        var card4 = new Card(Rank.Ace, Suit.Club);

        game.CurrentPlayer.Hand.AddCard(card1);
        game.CurrentPlayer.Hand.AddCard(card2);
        game.CurrentPlayer.Hand.AddCard(card3);
        game.CurrentPlayer.Hand.AddCard(card4);
        game.EndPlayPhase();
        game.DiscardFromHand(card4);
        
        var meld = game.CreateMeldIfValid(player, MeldType.Set, new List<Card> { card1, card2, card3 });
        Assert.NotNull(meld);
        Assert.NotEqual(player, game.CurrentPlayer);
    }
    [Fact]
    public void CreateMeldIfValid_CardsFromOtherPlayer_Rejected()
    {
        Game game = new Game();
        Player player1 = game.Players[0];
        Player player2 = game.Players[1];

        player1.Hand.ClearHand();
        player2.Hand.ClearHand();

        var card1 = new Card(Rank.Ace, Suit.Club);
        var card2 = new Card(Rank.Ace, Suit.Club);
        var card3 = new Card(Rank.Ace, Suit.Club);
        
        player1.Hand.AddCard(card1);
        player1.Hand.AddCard(card2);
        player1.Hand.AddCard(card3);
        
        var meld = game.CreateMeldIfValid(player2, MeldType.Set, new List<Card> { card1, card2, card3 });
        Assert.Null(meld);
    }
    [Fact]
    public void CreateMeldIfValid_Straight_Accepted()
    {
        Game game = new Game();
        Player player1 = game.Players[0];

        player1.Hand.ClearHand();
        
        var card1 = new Card(Rank.Ace, Suit.Club);
        var card2 = new Card(Rank.Two, Suit.Club);
        var card3 = new Card(Rank.Three, Suit.Club);
        var card4 = new Card(Rank.Four, Suit.Club);
        
        player1.Hand.AddCard(card1);
        player1.Hand.AddCard(card2);
        player1.Hand.AddCard(card3);
        player1.Hand.AddCard(card4);

        var candidateMeld = new List<Card> { card1, card2, card3, card4 };
        
        var meld = game.CreateMeldIfValid(player1, MeldType.Straight, candidateMeld);
        Assert.NotNull(meld);
        Assert.Equal(candidateMeld, player1.Hand.Cards);
        Assert.Equal(candidateMeld, meld.Cards);
        Assert.Equal(player1, meld.Owner);
        Assert.Equal(MeldType.Straight, meld.Type);
    }    
    [Fact]
    public void CreateMeldIfValid_Straight_WrongSuit_Rejected()
    {
        Game game = new Game();
        Player player1 = game.Players[0];

        player1.Hand.ClearHand();
        
        var card1 = new Card(Rank.Ace, Suit.Club);
        var card2 = new Card(Rank.Two, Suit.Club);
        var card3 = new Card(Rank.Three, Suit.Heart);
        var card4 = new Card(Rank.Four, Suit.Club);
        
        player1.Hand.AddCard(card1);
        player1.Hand.AddCard(card2);
        player1.Hand.AddCard(card3);
        player1.Hand.AddCard(card4);

        int handCount = player1.Hand.Count;
        var candidateMeld = new List<Card> { card1, card2, card3, card4 };
        
        var meld = game.CreateMeldIfValid(player1, MeldType.Straight, candidateMeld);
        Assert.Null(meld);
        Assert.Equal(handCount, player1.Hand.Count);
    }
    [Fact]
    public void EndRound_AfterRoundOne_AdvancesToRoundTwoAndAwardsPoints()
    {
        var game = new Game();
        
        game.DrawFromDeck();

        var roundOne = game.CurrentRound;
        var winner = game.CurrentPlayer;
        
        winner.Hand.ClearHand();
        
        var expectedPoints = game.Players.ToDictionary(
            player => player,
            player => player.Points + player.Hand.GetHandValue()
        );

        game.EndPlayPhase();

        Assert.Equal(winner, roundOne.Winner);

        foreach (var player in game.Players)
        {
            Assert.Equal(expectedPoints[player], player.Points);
        }

        Assert.Equal(2, game.CurrentRound.RoundNumber);
        Assert.False(game.IsGameOver);
        Assert.Equal(TurnPhase.Draw, game.CurrentTurnPhase);
    }
    
    [Fact]
    public void EndRound_AfterFinalRound_EndsGameAndDoesNotDealNewHands()
    {
        var game = new Game();

        game.DrawFromDeck();
        game.CurrentPlayer.Hand.ClearHand();
        game.EndPlayPhase();

        Assert.Equal(2, game.CurrentRound.RoundNumber);
        
        game.DrawFromDeck();

        var roundTwo = game.CurrentRound;
        var winner = game.CurrentPlayer;

        winner.Hand.ClearHand();

        var expectedPoints = game.Players.ToDictionary(
            player => player,
            player => player.Points + player.Hand.GetHandValue()
        );
        
        var handsBeforeGameEnd = game.Players.ToDictionary(
            player => player,
            player => player.Hand.Cards.ToList()
        );

        game.EndPlayPhase();

        Assert.Equal(winner, roundTwo.Winner);

        foreach (var player in game.Players)
        {
            Assert.Equal(expectedPoints[player], player.Points);
            Assert.Equal(handsBeforeGameEnd[player], player.Hand.Cards);
        }

        Assert.Same(roundTwo, game.CurrentRound);
        Assert.True(game.IsGameOver);
    }
    
    [Fact]
    public void EndPlayPhase_AfterGameOver_ThrowsGameOverException()
    {
        var game = new Game();
        
        game.DrawFromDeck();
        game.CurrentPlayer.Hand.ClearHand();
        game.EndPlayPhase();
        
        game.DrawFromDeck();
        game.CurrentPlayer.Hand.ClearHand();
        game.EndPlayPhase();

        Assert.True(game.IsGameOver);

        var exception = Assert.Throws<InvalidOperationException>(
            () => game.EndPlayPhase()
        );

        Assert.Equal("Game is Over", exception.Message);
    }
    
    [Fact]
    public void Standings_OrdersPlayersByLowestPoints()
    {
        var game = new Game();

        var player1 = game.Players[0];
        var player2 = game.Players[1];
        var player3 = game.Players[2];
        var player4 = game.Players[3];

        player1.AddPoints(40);
        player2.AddPoints(10);
        player3.AddPoints(30);
        player4.AddPoints(20);

        Assert.Equal(
            new List<Player> { player2, player4, player3, player1 },
            game.Standings
        );
    }
    [Fact]
    public void CreateMeldIfValid_StraightInInvalidOrder_Rejected()
    {
        var game = new Game();
        var player = game.Players[0];

        player.Hand.ClearHand();

        var card1 = new Card(Rank.Ace, Suit.Club);
        var card2 = new Card(Rank.Two, Suit.Club);
        var card3 = new Card(Rank.Three, Suit.Club);
        var card4 = new Card(Rank.Four, Suit.Club);

        player.Hand.AddCard(card1);
        player.Hand.AddCard(card2);
        player.Hand.AddCard(card3);
        player.Hand.AddCard(card4);

        var meld = game.CreateMeldIfValid(
            player,
            MeldType.Straight,
            new List<Card> { card4, card2, card3, card1 }
        );

        Assert.Null(meld);
    }
    [Fact]
    public void CreateMeldIfValid_Straight_JokerAtStart_AcceptedAndOrderPreserved()
    {
        var game = new Game();
        var player = game.Players[0];

        player.Hand.ClearHand();

        var joker = new Card(Rank.Joker, Suit.Joker);
        var two = new Card(Rank.Two, Suit.Club);
        var three = new Card(Rank.Three, Suit.Club);
        var four = new Card(Rank.Four, Suit.Club);

        foreach (var card in new[] { joker, two, three, four })
            player.Hand.AddCard(card);

        var candidate = new List<Card> { joker, two, three, four };

        var meld = game.CreateMeldIfValid(player, MeldType.Straight, candidate);

        Assert.NotNull(meld);
        Assert.Equal(candidate, meld.Cards);
    }
    [Fact]
    public void CreateMeldIfValid_Straight_JokerAtEnd_AcceptedAndOrderPreserved()
    {
        var game = new Game();
        var player = game.Players[0];

        player.Hand.ClearHand();

        var two = new Card(Rank.Two, Suit.Club);
        var three = new Card(Rank.Three, Suit.Club);
        var four = new Card(Rank.Four, Suit.Club);
        var joker = new Card(Rank.Joker, Suit.Joker);

        foreach (var card in new[] { two, three, four, joker })
            player.Hand.AddCard(card);

        var candidate = new List<Card> { two, three, four, joker };

        var meld = game.CreateMeldIfValid(player, MeldType.Straight, candidate);

        Assert.NotNull(meld);
        Assert.Equal(candidate, meld.Cards);
    }
}
