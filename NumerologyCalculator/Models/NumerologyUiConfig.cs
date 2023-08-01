namespace NumerologyCalculator.Models;

public record NumerologyUiConfig
{
    public int MaxInputChars { get; set; } = 1_000;

    public int UiInputDelay { get; set; } = 1_000;

    public string CalculatorEquationSeparator { get; set; } = " + ";

    public string CalculatorEquationCombinedItemTemplate { get; set; } = "({0}: {1})";
}
