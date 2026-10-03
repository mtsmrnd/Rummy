using Rummy;

namespace Rummy.Tests;

public class CardTests
{
    [Fact]
    public void RankAndSuitConstructTest()
    {
        Card card = new Card(Rank.Ace, Suit.Heart);
        Assert.Equal(Rank.Ace, card.Rank);
        Assert.Equal(Suit.Heart, card.Suit);
    }
    [Fact]
    public void CardAcePointsIs20()
    {
        Card card = new Card(Rank.Ace, Suit.Heart);
        Assert.Equal(20, card.Value);
    }
    [Fact]
    public void CardJokerPointsIs30()
    {
        Card card = new Card(Rank.Joker, Suit.Joker);
        Assert.Equal(30, card.Value);
    }
    [Fact]
    public void CardToStringTest()
    {
        Card card = new Card(Rank.Ace, Suit.Heart);
        Card card2 = new Card(Rank.Joker, Suit.Joker);
        Assert.Equal("A♥", card.ToString());
        Assert.Equal("Joker", card2.ToString());
    }

    [Theory]
    [InlineData(Rank.Ten)]
    [InlineData(Rank.Jack)]
    [InlineData(Rank.Queen)]
    [InlineData(Rank.King)]

    public void CardFacesPointsIs10(Rank rank)
    {
        Card card = new Card(rank, Suit.Heart);
        Assert.Equal(10, card.Value);
    }

}
