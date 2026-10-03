using Rummy;

namespace Rummy.Tests;

public class HandTests
{
    [Fact]
    public void NewHandIsEmpty()
    {
        Hand hand = new Hand();
        Assert.Empty(hand.Cards);
    }
    [Fact]
    public void AddCardSameObjectAndAmount()
    {
        Hand hand = new Hand();
        Card card = new Card(Rank.Joker, Suit.Joker);

        hand.AddCard(card);
        Assert.Same(card, hand.Cards[0]);
        Assert.Equal(1, hand.Count);
    }
    [Fact]
    public void RemoveCardReturnsSameObject()
    {
        Hand hand = new Hand();
        Card card = new Card(Rank.Joker, Suit.Joker);

        hand.AddCard(card);
        Assert.Same(card, hand.RemoveCard(card));
        Assert.Empty(hand.Cards);
    }
    [Fact]
    public void RemoveCardNotInHandThrows()
    {
        Hand hand = new Hand();
        Card card = new Card(Rank.Joker, Suit.Joker);
        Card card2 = new Card(Rank.Joker, Suit.Joker);

        hand.AddCard(card);
        Assert.Throws<InvalidOperationException>(() => hand.RemoveCard(card2));
    }
    [Fact]
    public void MoveCard_LastToFirst_Moved()
    {
        Hand hand = new Hand();
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Joker, Suit.Joker);
        Card card3 = new Card(Rank.Two, Suit.Club);

        hand.AddCard(card1);
        hand.AddCard(card2);
        hand.AddCard(card3);

        hand.MoveCard(2, 0);
        Assert.Equal(new List<Card>{card3, card1, card2}, hand.Cards);
    }
    [Fact]
    public void MoveCard_FirstToLast_Moved()
    {
        Hand hand = new Hand();
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Joker, Suit.Joker);
        Card card3 = new Card(Rank.Two, Suit.Club);

        hand.AddCard(card1);
        hand.AddCard(card2);
        hand.AddCard(card3);

        hand.MoveCard(0, 2);
        
        Assert.Equal(new List<Card>{card2, card3, card1}, hand.Cards);
    }
    
    [Fact]
    public void MoveCard_MiddleIndices_Moved()
    {
        Hand hand = new Hand();
        Card card = new Card(Rank.Ace, Suit.Club);
        Card card1 = new Card(Rank.Two, Suit.Club);
        Card card2 = new Card(Rank.Three, Suit.Club);
        Card card3 = new Card(Rank.Four, Suit.Club);
        Card card4 = new Card(Rank.Five, Suit.Club);
       
        hand.AddCard(card);
        hand.AddCard(card1);
        hand.AddCard(card2);
        hand.AddCard(card3);
        hand.AddCard(card4);

        hand.MoveCard(1, 3);
        
        Assert.Equal(new List<Card>{card, card2, card3, card1, card4}, hand.Cards);
    }
    [Fact]
    public void MoveCard_LIndexOutOfBounds_Throws()
    {
        Hand hand = new Hand();
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Joker, Suit.Joker);
        Card card3 = new Card(Rank.Two, Suit.Club);

        hand.AddCard(card1);
        hand.AddCard(card2);
        hand.AddCard(card3);
        Assert.Throws<ArgumentOutOfRangeException>(() => hand.MoveCard(3, 0));
    }

    [Fact]
    public void GetHandValueEmpty_EqualsZero()
    {
        Hand hand = new Hand();
        
        Assert.Equal(0, hand.GetHandValue());
    }
    [Fact]
    public void GetHandValueTest()
    {
        Hand hand = new Hand();
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Joker, Suit.Joker);
        Card card3 = new Card(Rank.Two, Suit.Club);
        Card card4 = new Card(Rank.King, Suit.Club);

        hand.AddCard(card1);
        hand.AddCard(card2);
        hand.AddCard(card3);
        hand.AddCard(card4);
        Assert.Equal(62, hand.GetHandValue());
    }
}