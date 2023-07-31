namespace NumerologyCalculator.Services;

public class NumerologyDigitCalculatorService
{
    private const int _charCodeDelta = 48;
    private readonly NumerologyUiService _numerologyUiService;

    public NumerologyDigitCalculatorService(NumerologyUiService numerologyUiService) =>
        _numerologyUiService = numerologyUiService;

    public async Task<(string result, IEnumerable<(string equation, string sum, int numberOfDigits, string sequence)> steps)> Calculate(string text, CancellationToken cancellationToken = default) =>
        await Task.Run(() =>
            {
                try
                {
                    var steps = new List<(string equation, string sum, int numberOfDigits, string sequence)>();
                    var workingCollection =
                        text.Where(x => char.IsDigit(x))
                            .Select(x => (int)x - _charCodeDelta)
                            .ToList();

                    if (workingCollection is { Count: 0 })
                        return (result: string.Empty, steps);

                    var result = workingCollection.Sum().ToString();

                    steps.Add(
                        (
                            equation: _numerologyUiService.ComposeCalculatorEntryEquation(workingCollection),
                            sum: result,
                            numberOfDigits: workingCollection.Count,
                            sequence: _numerologyUiService.ComposeCalculatorEntrySequence(workingCollection)
                        )
                    );

                    while (result is { Length: > 1 })
                    {
                        workingCollection = result.Select(x => (int)x - _charCodeDelta).ToList();
                        result = workingCollection.Sum().ToString();

                        steps.Add(
                            (
                                equation: _numerologyUiService.ComposeCalculatorEntryEquation(workingCollection),
                                sum: result,
                                numberOfDigits: workingCollection.Count,
                                sequence: _numerologyUiService.ComposeCalculatorEntrySequence(workingCollection)
                            )
                        );
                    }

                    return (result, steps);
                }
                catch
                {
                    return (
                        result: string.Empty,
                        steps: Enumerable.Empty<(string equation, string sum, int numberOfDigits, string sequence)>()
                    );
                }
            },
            cancellationToken
        );
}
