using NumerologyCalculator.Interfaces;
using NumerologyCalculator.Models;

namespace NumerologyCalculator.Services;

public class NumerologyDigitCalculatorService : INumerologyDigitCalculatorService
{
    private const int _charCodeDelta = 48;
    private readonly INumerologyUiService _numerologyUiService;

    public NumerologyDigitCalculatorService(INumerologyUiService numerologyUiService) =>
        _numerologyUiService = numerologyUiService;

    public async Task<CalculationResultModel> Calculate(string? text, CancellationToken cancellationToken = default) =>
        await Task.Run<CalculationResultModel>(() =>
            {
                if (text is not { Length: > 0 })
                    return new(Result: string.Empty, Steps: Enumerable.Empty<CalculationStepModel>());

                var workingCollection =
                    text.Where(x => char.IsDigit(x))
                        .Select(x => x - _charCodeDelta)
                        .ToList();

                if (workingCollection.Count == 0)
                    return new(Result: string.Empty, Steps: Enumerable.Empty<CalculationStepModel>());

                string result;
                var steps = new List<CalculationStepModel>
                {
                    new(
                        Equation: _numerologyUiService.ComposeCalculatorEntryEquation(workingCollection),
                        Sum: result = workingCollection.Sum().ToString(),
                        NumberOfCharacters: workingCollection.Count,
                        Sequence: _numerologyUiService.ComposeCalculatorEntrySequence(workingCollection)
                    )
                };

                while (result.Length > 1)
                {
                    workingCollection = result.Select(x => x - _charCodeDelta).ToList();

                    steps.Add(
                        new(
                            Equation: _numerologyUiService.ComposeCalculatorEntryEquation(workingCollection),
                            Sum: result = workingCollection.Sum().ToString(),
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
