using NumerologyCalculator.Services;

namespace NumerologyCalculator.Tests.Services;

public class NumerologyLinksServiceTests
{
    private readonly NumerologyLinksService _numerologyLinksService;

    public NumerologyLinksServiceTests() => _numerologyLinksService = new(new());

    [Fact]
    public void NumerologyLinksService_IsNumerologyLinkEligible_False_When_Null()
    {
        Assert.False(_numerologyLinksService.IsNumerologyLinkEligible(null, out var result));
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void NumerologyLinksService_IsNumerologyLinkEligible_False_When_Empty()
    {
        Assert.False(_numerologyLinksService.IsNumerologyLinkEligible(string.Empty, out var result));
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void NumerologyLinksService_IsNumerologyLinkEligible_False_When_Out_Of_Bounds()
    {
        Assert.False(_numerologyLinksService.IsNumerologyLinkEligible("12345", out var result));
        Assert.Equal("12345", result);
    }

    [Fact]
    public void NumerologyLinksService_IsNumerologyLinkEligible_True_When_Between_Bounds()
    {
        Assert.True(_numerologyLinksService.IsNumerologyLinkEligible("1", out var result));
        Assert.Equal("1", result);
    }

    [Fact]
    public void NumerologyLinksService_GetNumerologyUrl_Url_When_Empty() =>
        Assert.Equal("/", _numerologyLinksService.GetNumerologyUrl(string.Empty));

    [Fact]
    public void NumerologyLinksService_GetNumerologyUrl_Url_When_Text() =>
        Assert.Equal("/hello", _numerologyLinksService.GetNumerologyUrl("hello"));
}
