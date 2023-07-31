using NumerologyCalculator.Models;

namespace NumerologyCalculator.Services;

public class NumerologyUiService
{
    private readonly NumerologyUiConfig _config;

    public NumerologyUiService(NumerologyUiConfig config) =>
        _config = config;

    public string GetLiCalculatorEntryClass(int i) => i switch
    {
        > 0 => _config.LiCalculatorEntryClass,
        _ => string.Empty
    };

    public string ComposeCalculatorEntryEquation<T>(IEnumerable<T> collection) =>
        string.Join(_config.CalculatorEquationSeparator, collection);

    public string ComposeCalculatorEntrySequence<T>(IEnumerable<T> collection) =>
        string.Join(string.Empty, collection);
}
