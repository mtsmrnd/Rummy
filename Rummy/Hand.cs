using System.Text;

namespace Rummy;

public class Hand
{
	private List<Card> _cardsInHand = new List<Card>();
	public IReadOnlyList<Card> Cards => _cardsInHand;
	public int Count => _cardsInHand.Count;

	public void AddCard(Card card)
	{
		_cardsInHand.Add(card);
	}

	public Card RemoveCard(Card card)
	{
		if (Count == 0) throw new InvalidOperationException("Hand is empty");
		if (!_cardsInHand.Contains(card)) throw new InvalidOperationException("Card is not in hand");
		_cardsInHand.Remove(card);
		return card;
	}

	public void ClearHand() => _cardsInHand.Clear();

	public override string ToString()
	{
		StringBuilder hand = new StringBuilder();
		for (int i = 0; i < Count; i++)
		{
			hand.Append(_cardsInHand[i]);
			if (i != Count - 1)
			{
				hand.Append(" / ");
			}
		}
		return hand.ToString();
	}
}
