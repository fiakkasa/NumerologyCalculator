using NumerologyCalculator.Services;

namespace NumerologyCalculator.Tests.Services;

public class NumerologyUiServiceTests
{
    private readonly NumerologyUiService _numerologyUiService;

    public NumerologyUiServiceTests() => _numerologyUiService = new(new(MaxInputChars: 5));

    [Fact]
    public void NumerologyUiServiceTests_ComposeCalculatorEntryEquation_String_With_Collection() =>
        Assert.Equal("1 + 2", _numerologyUiService.ComposeCalculatorEntryEquation(new[] { 1, 2 }));

    [Fact]
    public void NumerologyUiServiceTests_ComposeCalculatorEntrySequence_String_With_Collection() =>
        Assert.Equal("12", _numerologyUiService.ComposeCalculatorEntrySequence(new[] { 1, 2 }));

    [Fact]
    public void NumerologyUiServiceTests_ComposeCalculatorEquationCombinedItem_String_With_Inputs() =>
        Assert.Equal("(A: 2)", _numerologyUiService.ComposeCalculatorEquationCombinedItem("A", "2"));

    [Fact]
    public void NumerologyUiServiceTests_InputDelay_Task_With_CancellationToken() =>
        Assert.IsAssignableFrom<Task>(_numerologyUiService.InputDelay(CancellationToken.None));

    [Fact]
    public void NumerologyUiServiceTests_NormalizeTextInput_Empty_When_Null() =>
        Assert.Equal(string.Empty, _numerologyUiService.NormalizeTextInput(null));

    [Fact]
    public void NumerologyUiServiceTests_NormalizeTextInput_Empty_When_Empty() =>
        Assert.Equal(string.Empty, _numerologyUiService.NormalizeTextInput(string.Empty));

    [Fact]
    public void NumerologyUiServiceTests_NormalizeTextInput_Text_When_Within_Bounds() =>
        Assert.Equal("hello", _numerologyUiService.NormalizeTextInput(" he l lo   "));

    [Fact]
    public void NumerologyUiServiceTests_NormalizeTextInput_Redacted_Text_When_Out_Of_Bounds() =>
        Assert.Equal("hello", _numerologyUiService.NormalizeTextInput("hello world"));
}
