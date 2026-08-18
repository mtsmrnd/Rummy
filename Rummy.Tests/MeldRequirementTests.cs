using Rummy;

namespace Rummy.Tests;

public class MeldRequirementTests
{
    [Theory]
    [InlineData(MeldType.Set, 3)]
    [InlineData(MeldType.Straight, 4)]
    [InlineData(MeldType.Straight, 13)]
    public void MeldCreationSuccess(MeldType type, int size)
    {
        MeldRequirement meldRequirement = new MeldRequirement(type, size);
        Assert.Equal(type, meldRequirement.MeldType);
        Assert.Equal(size, meldRequirement.InitialSize);
    }
    [Theory]
    [InlineData(MeldType.Set, 4)]
    [InlineData(MeldType.Straight, 5)]
    public void MeldCreationThrows(MeldType type, int size)
    {
        Assert.Throws<ArgumentException>(() => new MeldRequirement(type, size));
    }
}