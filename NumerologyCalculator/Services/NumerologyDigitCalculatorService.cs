using NumerologyCalculator.Interfaces;
using NumerologyCalculator.Models;

namespace NumerologyCalculator.Services;

public class NumerologyDigitCalculatorService : INumerologyDigitCalculatorService
{
    private const int _charCodeDelta = 48;
    private readonly INumerologyUiService _numerologyUiService;

    public NumerologyDigitCalculatorService(INumerologyUiService numerologyUiService) =>
        _numerologyUiService = numerologyUiService;

    public async Task<CalculationResult> Calculate(string text, CancellationToken cancellationToken = default) =>
        await Task.Run<CalculationResult>(() =>
            {
                var steps = new List<CalculationStep>();
                var workingCollection =
                    text.Where(x => char.IsDigit(x))
                        .Select(x => x - _charCodeDelta)
                        .ToList();

                if (workingCollection is { Count: 0 })
                    return new(Result: string.Empty, Steps: steps);

                var result = workingCollection.Sum().ToString();

                steps.Add(
                    new(
                        Equation: _numerologyUiService.ComposeCalculatorEntryEquation(workingCollection),
                        Sum: result,
                        NumberOfCharacters: workingCollection.Count,
                        Sequence: _numerologyUiService.ComposeCalculatorEntrySequence(workingCollection)
                    )
                );

                while (result is { Length: > 1 })
                {
                    workingCollection = result.Select(x => x - _charCodeDelta).ToList();
                    result = workingCollection.Sum().ToString();

                    steps.Add(
                        new(
                            Equation: _numerologyUiService.ComposeCalculatorEntryEquation(workingCollection),
                            Sum: result,
                            NumberOfCharacters: workingCollection.Count,
                            Sequence: _numerologyUiService.ComposeCalculatorEntrySequence(workingCollection)
                        )
                    );
                }

                return new(Result: result, Steps: steps);
            },
            cancellationToken
        );
}
