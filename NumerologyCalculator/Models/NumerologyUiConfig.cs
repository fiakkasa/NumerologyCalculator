namespace NumerologyCalculator.Models;

public record NumerologyUiConfig(
    int MaxInputChars = 1_000,
    int UiInputDelay = 1_000,
    string CalculatorEquationSeparator = " + ",
    string CalculatorEquationCombinedItemTemplate = "({0}: {1})"
);
