using Rummy;

namespace Rummy.Tests;

public class DeckTests
{
    [Fact]
    public void NewSingleDeckContains54Cards()
    {
        Deck deck = new Deck(1);

        Assert.Equal(54, deck.Count);
    }
    [Fact]
    public void NewDoubleDeckContains108Cards()
    {
        Deck deck = new Deck(2);

        Assert.Equal(108, deck.Count);
    }
    [Fact]
    public void DrawCardReducesDeck()
    {
        Deck deck = new Deck(1);
        deck.DrawCard();
        Assert.Equal(53, deck.Count);
    }
    [Fact]
    public void DrawFromEmptyDeckThrowsException()
    {
        Deck deck = new Deck(0);
        
        Assert.Throws<InvalidOperationException>(() => deck.DrawCard());
    }

}