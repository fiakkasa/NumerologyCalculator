﻿using NumerologyCalculator.Models;

namespace NumerologyCalculator.Services;

public class NumerologyDigitCalculatorService
{
    private const int _charCodeDelta = 48;
    private readonly NumerologyUiService _numerologyUiService;

    public NumerologyDigitCalculatorService(NumerologyUiService numerologyUiService) =>
        _numerologyUiService = numerologyUiService;

    public async Task<CalculationResult> Calculate(string text, CancellationToken cancellationToken = default) =>
        await Task.Run<CalculationResult>(() =>
            {
                try
                {
                    var steps = new List<CalculationStep>();
                    var workingCollection =
                        text.Where(x => char.IsDigit(x))
                            .Select(x => (int)x - _charCodeDelta)
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
                        workingCollection = result.Select(x => (int)x - _charCodeDelta).ToList();
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
                }
                catch
                {
                    return new(Result: string.Empty, Steps: Enumerable.Empty<CalculationStep>());
                }
            },
            cancellationToken
        );
}
