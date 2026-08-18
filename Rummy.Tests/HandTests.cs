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
}