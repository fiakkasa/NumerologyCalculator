
using NumerologyCalculator.Interfaces;
using NumerologyCalculator.Services;

namespace NumerologyCalculator.Tests.Services;

public class NumerologyDigitCalculatorServiceTests
{
    private readonly Mock<INumerologyUiService> _mockNumerologyUiService;
    private readonly NumerologyDigitCalculatorService _numerologyDigitCalculatorService;

    public NumerologyDigitCalculatorServiceTests()
    {
        _mockNumerologyUiService = new Mock<INumerologyUiService>();

        _mockNumerologyUiService
            .Setup(m => m.ComposeCalculatorEntryEquation(It.IsAny<List<int>>()))
            .Returns("1 + 2 + 3");

        _mockNumerologyUiService
            .Setup(m => m.ComposeCalculatorEntrySequence(It.IsAny<List<int>>()))
            .Returns("123");

        _numerologyDigitCalculatorService = new(_mockNumerologyUiService.Object);
    }

    [Fact]
    public async Task NumerologyDigitCalculatorService_Calculate_Throws_When_Cancelled()
    {
        var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<TaskCanceledException>(async () => await _numerologyDigitCalculatorService.Calculate(string.Empty, cts.Token));
    }

    [Fact]
    public async Task NumerologyDigitCalculatorService_Calculate_Empty_When_Empty()
    {
        var result = await _numerologyDigitCalculatorService.Calculate(string.Empty);

        Assert.Equal(string.Empty, result.Result);
        Assert.Empty(result.Steps);
    }

    [Fact]
    public async Task NumerologyDigitCalculatorService_Calculate_To_Single_Digit_And_Single_Step_When_Single_Digit()
    {
        var result = await _numerologyDigitCalculatorService.Calculate("1");

        Assert.Equal("1", result.Result);
        Assert.Single(result.Steps);
    }

    [Fact]
    public async Task NumerologyDigitCalculatorService_Calculate_To_Single_Digit_And_Single_Step_When_Multiple_Digits()
    {
        var result = await _numerologyDigitCalculatorService.Calculate("123");

        Assert.Equal("6", result.Result);
        Assert.Single(result.Steps);
    }

    [Fact]
    public async Task NumerologyDigitCalculatorService_Calculate_To_Single_Digit_And_Multiple_Steps_When_Multiple_Digits()
    {
        var result = await _numerologyDigitCalculatorService.Calculate("1234");

        Assert.Equal("1", result.Result);
        Assert.Equal(2, result.Steps.Count());
    }

    [Fact]
    public async Task NumerologyDigitCalculatorService_Calculate_To_Single_Digit_And_Multiple_Steps_When_Input_Mixed()
    {
        var result = await _numerologyDigitCalculatorService.Calculate("  1  ! 2 a3 C4  -");

        Assert.Equal("1", result.Result);
        Assert.Equal(2, result.Steps.Count());
    }
}