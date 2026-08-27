namespace Rummy;

public class Meld
{
	public MeldType Type { get; }
	public Player Owner { get; }
    private List<Card> _cardsInMeld;
    public IReadOnlyList<Card> Cards => _cardsInMeld;

    public Meld(MeldType type, Player owner, IEnumerable<Card> cards)
	{
		Type = type;
		Owner = owner;
		_cardsInMeld = new List<Card>(cards);
	}

	public bool AddCard(Card card, MeldSide? side)
	{
		if (Type == MeldType.Set && side == null)
		{
			_cardsInMeld.Add(card);
			return true;
		}
		if (Type == MeldType.Straight && side != null)
		{
			if (side == MeldSide.Right)
			{
				_cardsInMeld.Add(card);
				return true;
			}
			if (side == MeldSide.Left)
			{
				_cardsInMeld.Insert(0, card);
				return true;
			}
		}
		return false;
	}


}
