
using NumerologyCalculator.Interfaces;
using NumerologyCalculator.Services;

namespace NumerologyCalculator.Tests.Services;

public class NumerologyLetterCalculatorServiceTests
{
    private readonly INumerologyUiService _mockNumerologyUiService;
    private readonly NumerologyLetterCalculatorService _numerologyLetterCalculatorService;

    public NumerologyLetterCalculatorServiceTests()
    {
        _mockNumerologyUiService = Substitute.For<INumerologyUiService>();

        _mockNumerologyUiService
            .ComposeCalculatorEntryEquation(Arg.Any<string[]>())
            .Returns("1 + 2 + 3");

        _mockNumerologyUiService
            .ComposeCalculatorEntryEquation(Arg.Any<int[]>())
            .Returns("1 + 2");

        _mockNumerologyUiService
            .ComposeCalculatorEntrySequence(Arg.Any<int[]>())
            .Returns("123", "12");

        _mockNumerologyUiService
            .ComposeCalculatorEquationCombinedItem(Arg.Any<string>(), Arg.Any<string>())
            .Returns("(A: 1)");

        _numerologyLetterCalculatorService = new(_mockNumerologyUiService);
    }

    [Fact]
    public async Task NumerologyLetterCalculatorService_Calculate_Throws_When_Cancelled()
    {
        var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<TaskCanceledException>(async () => await _numerologyLetterCalculatorService.Calculate(string.Empty, cts.Token));
    }

    [Fact]
    public async Task NumerologyLetterCalculatorService_Calculate_Empty_When_Null()
    {
        var result = await _numerologyLetterCalculatorService.Calculate(null);

        Assert.Equal(string.Empty, result.Result);
        Assert.Empty(result.Steps);
    }

    [Fact]
    public async Task NumerologyLetterCalculatorService_Calculate_Empty_When_Empty()
    {
        var result = await _numerologyLetterCalculatorService.Calculate(string.Empty);

        Assert.Equal(string.Empty, result.Result);
        Assert.Empty(result.Steps);
    }

    [Fact]
    public async Task NumerologyLetterCalculatorService_Calculate_Empty_When_No_Letters()
    {
        var result = await _numerologyLetterCalculatorService.Calculate(" 1 ");

        Assert.Equal(string.Empty, result.Result);
        Assert.Empty(result.Steps);
    }

    [Fact]
    public async Task NumerologyLetterCalculatorService_Calculate_To_Single_Letter_And_Single_Step_When_Single_Letter()
    {
        var result = await _numerologyLetterCalculatorService.Calculate("A");

        Assert.Equal("1", result.Result);
        Assert.Single(result.Steps);
    }

    [Fact]
    public async Task NumerologyLetterCalculatorService_Calculate_To_Single_Letter_And_Single_Step_When_Multiple_Letters()
    {
        var result = await _numerologyLetterCalculatorService.Calculate("ABC");

        Assert.Equal("6", result.Result);
        Assert.Single(result.Steps);
    }

    [Fact]
    public async Task NumerologyLetterCalculatorService_Calculate_To_Single_Letter_And_Multiple_Steps_When_Multiple_Letters()
    {
        var result = await _numerologyLetterCalculatorService.Calculate("ABCD");

        Assert.Equal("1", result.Result);
        Assert.Equal(2, result.Steps.Length);
    }

    [Fact]
    public async Task NumerologyLetterCalculatorService_Calculate_To_Single_Letter_And_Multiple_Steps_When_Input_Mixed()
    {
        var result = await _numerologyLetterCalculatorService.Calculate("  A  ! 2 B3 C4  - D");

        Assert.Equal("1", result.Result);
        Assert.Equal(2, result.Steps.Length);
    }
}