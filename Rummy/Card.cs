namespace Rummy;

public class Card
{
    public Rank Rank { get; }
    public Suit Suit { get; }
    public int Value
    {
        get => Rank switch
        {
            Rank.Ace => 20,
            Rank.Two => 2,
            Rank.Three => 3,
            Rank.Four => 4,
            Rank.Five => 5,
            Rank.Six => 6,
            Rank.Seven => 7,
            Rank.Eight => 8,
            Rank.Nine => 9,
            Rank.Ten or Rank.Jack or Rank.Queen or Rank.King => 10,
            Rank.Joker => 30,
            _ => throw new ArgumentOutOfRangeException("Card Rank is invalid")
        };
    }

    public Card(Rank rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public override string ToString()
    {
        if (Rank == Rank.Joker)
        {
            return $"{Rank}";
        }
        return $"{Rank} of {Suit}";
    }

}
