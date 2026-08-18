namespace Rummy;

public class MeldRequirement
{
	public MeldType MeldType { get; }
	public int InitialSize { get; }

	public MeldRequirement(MeldType type, int initialSize)
	{
		MeldType = type;
		InitialSize = initialSize;
		if (MeldType == MeldType.Set && initialSize != 3)
		{
			throw new ArgumentException("Sets must be of initial size 3");
		}
		if (MeldType == MeldType.Straight && initialSize != 4 && initialSize != 13)
		{
            throw new ArgumentException("Straights must be of initial size 4 or 13");
        }
    }

}
