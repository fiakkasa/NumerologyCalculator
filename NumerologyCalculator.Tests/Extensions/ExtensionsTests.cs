namespace NumerologyCalculator.Extensions.Tests;

public class ExtensionsTests
{
    [Fact]
    public void Extensions_ToDeltaInt_Digit() =>
        Assert.Equal(1, '1'.ToDeltaInt());

    [Fact]
    public void Extensions_ToDeltaInt_Letter() =>
        Assert.Equal(17, 'A'.ToDeltaInt());

    [Fact]
    public void Extensions_ToDeltaIntCollectionSequence_Empty() =>
        Assert.Empty(string.Empty.ToDeltaIntCollectionSequence());

    [Fact]
    public void Extensions_ToDeltaIntCollectionSequence_With_Digits() =>
        Assert.True("123".ToDeltaIntCollectionSequence() is [1, 2, 3]);

    [Fact]
    public void Extensions_ToDeltaIntCollectionSequence_With_Mixed_Text() =>
        Assert.True("1A2B".ToDeltaIntCollectionSequence() is [1, 17, 2, 18]);

    [Fact]
    public void Extensions_ToSumString_Empty() =>
        Assert.Equal("0", Array.Empty<int>().ToSumString());

    [Fact]
    public void Extensions_ToSumString_Positive() =>
        Assert.Equal("6", new[] { 1, 2, 3 }.ToSumString());

    [Fact]
    public void Extensions_ToSumString_Negatives() =>
        Assert.Equal("-6", new[] { -1, -2, -3 }.ToSumString());

    [Fact]
    public void Extensions_ToSumString_Mixed() =>
        Assert.Equal("2", new[] { 1, -2, 3 }.ToSumString());
}
