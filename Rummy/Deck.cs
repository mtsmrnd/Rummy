namespace Rummy;

public class Deck
{
    private List<Card> _cardsInDeck = new List<Card>();
    public IReadOnlyList<Card> Cards => _cardsInDeck;
    public int Count => _cardsInDeck.Count;

    public Deck(int numberOfDecks)
    {
        for (int i = 0; i < numberOfDecks; i++)
        {

            foreach (Suit suit in Enum.GetValues<Suit>())
            {
                if (suit == Suit.Joker)
                    continue;
                foreach (Rank rank in Enum.GetValues<Rank>())
                {
                    if (rank == Rank.Joker)
                        continue;
                    _cardsInDeck.Add(new Card(rank, suit));
                }
            }
            _cardsInDeck.Add(new Card(Rank.Joker, Suit.Joker));
            _cardsInDeck.Add(new Card(Rank.Joker, Suit.Joker));
        }
    }

    public Card DrawCard()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Deck doesn't have enough cards");
        }
        Card cardToDraw = _cardsInDeck[Count - 1];
        _cardsInDeck.RemoveAt(Count - 1);
        return cardToDraw;
    }

    public void ShuffleDeck()
    {
        for (int i = Count - 1; i > 0; i--)
        {
            int randomValue = Random.Shared.Next(0, i + 1);
            if (i == randomValue) continue;
            Card tempCard = _cardsInDeck[i];
            _cardsInDeck[i] = _cardsInDeck[randomValue];
            _cardsInDeck[randomValue] = tempCard;
        }
    }
}
