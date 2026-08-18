namespace Rummy;

public class Round
{
	public int RoundNumber { get; }
	private List<MeldRequirement> _meldRequirements;
	public IReadOnlyList<MeldRequirement> MeldRequirements => _meldRequirements;
	private List<Meld> _meldsInRound = new List<Meld>();
	public IReadOnlyList<Meld> Melds => _meldsInRound;
	public Player? Winner { get; private set; }

	public Round(int roundNumber, IEnumerable<MeldRequirement> requirements)
	{
		RoundNumber = roundNumber;
		_meldRequirements = new List<MeldRequirement>(requirements);
	}

	public void SetWinner(Player player)
	{
		if (Winner != null)
		{
			throw new InvalidOperationException("Round already has a winner");
		}
		Winner = player;
	}

	public void AddMeld(Meld meld)
	{
		_meldsInRound.Add(meld);
	}

}
