using Rummy;

namespace Rummy.Tests;

public class MeldTests
{
    [Fact]
    public void AddCard_Set_Null_Successful()
    {
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Ace, Suit.Spade);
        Card card3 = new Card(Rank.Ace, Suit.Diamond);
        
        Meld meld = new Meld(MeldType.Set, new Player("test"), new List<Card>{ card1, card2, card3});
        Card card = new Card(Rank.Ace, Suit.Club);
        bool result = meld.AddCard(card, null);
        Assert.True(result);
        Assert.Equal(new List<Card> {card1, card2, card3, card}, meld.Cards);
    }
    [Fact]
    public void AddCard_Set_NotNull()
    {
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Ace, Suit.Spade);
        Card card3 = new Card(Rank.Ace, Suit.Diamond);
        
        Meld meld = new Meld(MeldType.Set, new Player("test"), new List<Card>{ card1, card2, card3});
        Card card = new Card(Rank.Ace, Suit.Club);
        bool result = meld.AddCard(card, MeldSide.Left);
        Assert.False(result);
        Assert.Equal(new List<Card> {card1, card2, card3}, meld.Cards);
    } 
    [Fact]
    public void AddCard_Straight_Right()
    {
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        
        Meld meld = new Meld(MeldType.Straight, new Player("test"), new List<Card>{ card1, card2, card3, card4});
        Card card = new Card(Rank.Five, Suit.Club);
        bool result = meld.AddCard(card, MeldSide.Right);
        Assert.True(result);
        Assert.Equal(new List<Card> {card1, card2, card3, card4, card}, meld.Cards);
        Assert.Equal(card, meld.Cards[4]);
    }  
    [Fact]
    public void AddCard_Straight_Left()
    {
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        
        Meld meld = new Meld(MeldType.Straight, new Player("test"), new List<Card>{ card1, card2, card3, card4});
        Card card = new Card(Rank.King, Suit.Club);
        bool result = meld.AddCard(card, MeldSide.Left);
        Assert.True(result);
        Assert.Equal(new List<Card> {card, card1, card2, card3, card4}, meld.Cards);
        Assert.Equal(card, meld.Cards[0]);
    }
    [Fact]
    public void AddCard_Straight_Null()
    {
        Card card1 = new Card(Rank.Ace, Suit.Club);
        Card card2 = new Card(Rank.Two, Suit.Club);
        Card card3 = new Card(Rank.Three, Suit.Club);
        Card card4 = new Card(Rank.Four, Suit.Club);
        
        Meld meld = new Meld(MeldType.Straight, new Player("test"), new List<Card>{ card1, card2, card3, card4});
        Card card = new Card(Rank.King, Suit.Club);
        bool result = meld.AddCard(card, null);
        Assert.False(result);
        Assert.Equal(new List<Card> {card1, card2, card3, card4}, meld.Cards);
    }
    
    

}