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

    

}
