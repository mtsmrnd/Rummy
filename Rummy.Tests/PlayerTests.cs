using Rummy;

namespace Rummy.Tests;

public class PlayerTests
{
    private readonly Player _player;
    public PlayerTests()
    {
        _player = new Player("TestPlayer");
    }

    [Fact]
    public void PlayerConstructorTest()
    {

        Assert.Equal("TestPlayer", _player.Name);
        Assert.Equal(0, _player.Points);
        Assert.Equal(ObjectiveStatus.NotCompleted, _player.Status);
    }
    [Fact]
    public void PlayerAddPointSuccess()
    {
        _player.AddPoints(10);
        Assert.Equal(10, _player.Points);
    }
    [Fact]
    public void PlayerAddPointNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _player.AddPoints(-1));
    }
    [Fact]
    public void PlayerCompleteObjectiveTest()
    {
        _player.CompleteObjective();
        Assert.Equal(ObjectiveStatus.CompletedThisTurn, _player.Status);
    }
    [Fact]
    public void PlayerActivateObjectiveTest()
    {
        _player.CompleteObjective();
        _player.ActivateObjective();
        Assert.Equal(ObjectiveStatus.Active, _player.Status);
    }
    [Fact]
    public void PlayerCompleteObjectiveTestThrows()
    {
        _player.CompleteObjective();
        Assert.Throws<InvalidOperationException>(() => _player.CompleteObjective());
    }
    [Fact]
    public void PlayerActivateObjectiveTestThrows()
    {
        Assert.Throws<InvalidOperationException>(() => _player.ActivateObjective());
    }
    [Fact]
    public void ResetAfterCompletedReturnsToNotCompleted()
    {
        _player.CompleteObjective();
        _player.ResetObjective();
        Assert.Equal(ObjectiveStatus.NotCompleted, _player.Status);
    }
    [Fact]
    public void ResetAfterActiveReturnsToNotCompleted()
    {
        _player.CompleteObjective();
        _player.ActivateObjective();
        _player.ResetObjective();
        Assert.Equal(ObjectiveStatus.NotCompleted, _player.Status);
    }



}