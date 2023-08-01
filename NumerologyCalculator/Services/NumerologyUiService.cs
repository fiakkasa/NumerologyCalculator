using NumerologyCalculator.Models;

namespace NumerologyCalculator.Services;

public class NumerologyUiService
{
    private readonly NumerologyUiConfig _config;

    public NumerologyUiService(NumerologyUiConfig config) =>
        _config = config;

    public string GetLiCalculatorEntryClass(int i) => i switch
    {
        > 0 => _config.CalculatorStepEntryContainerClass,
        _ => string.Empty
    };

    public string ComposeCalculatorEntryEquation<T>(IEnumerable<T> collection) =>
        string.Join(_config.CalculatorEquationSeparator, collection);

    public string ComposeCalculatorEntrySequence<T>(IEnumerable<T> collection) =>
        string.Join(string.Empty, collection);

    public string ComposeCalculatorEquationCombinedItem<TLeft, TRight>(TLeft left, TRight right) =>
        string.Format(_config.CalculatorEquationCombinedItemTemplate, left, right);

    public string NormalizeTextInput(string? value)
    {
        if (value is { Length: > 0 } v)
        {
            return (v.Length > _config.MaxInputChars) switch
            {
                true => v[.._config.MaxInputChars],
                _ => v
            };
        }

        return string.Empty;
    }
}
