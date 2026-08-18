namespace Rummy;

public class Player
{
    public string Name { get; }
    public Hand Hand { get; } = new Hand();
    public int Points { get; private set; } = 0;
    public ObjectiveStatus Status { get; private set; } = ObjectiveStatus.NotCompleted;

    public Player(string name)
    {
        Name = name;
    }

    public void AddPoints(int points)
    {
        if (points < 0)
            throw new ArgumentOutOfRangeException("Points can't be negative");
        Points += points;
    }

    public void ResetObjective()
    {
        Status = ObjectiveStatus.NotCompleted;
    }

    public void CompleteObjective()
    {
        if (Status == ObjectiveStatus.NotCompleted)
        {
            Status = ObjectiveStatus.CompletedThisTurn;
        }
        else
        {
            throw new InvalidOperationException("Status is not NotCompleted");
        }
    }

    public void ActivateObjective()
    {
        if (Status == ObjectiveStatus.CompletedThisTurn)
        {
            Status = ObjectiveStatus.Active;
        }
        else
        {
            throw new InvalidOperationException("Status is not Completed");
        }
    }
}
