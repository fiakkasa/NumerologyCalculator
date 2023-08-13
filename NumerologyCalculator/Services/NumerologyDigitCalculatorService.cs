using NumerologyCalculator.Interfaces;
using NumerologyCalculator.Models;
using NumerologyCalculator.Extensions;
using System;
using System.Collections.Generic;

namespace NumerologyCalculator.Services;

public class NumerologyDigitCalculatorService : INumerologyDigitCalculatorService
{
    private readonly INumerologyUiService _numerologyUiService;

    public NumerologyDigitCalculatorService(INumerologyUiService numerologyUiService) =>
        _numerologyUiService = numerologyUiService;

    private static int[] ToFilteredDigitCollection(string text)
    {
        var count = 0;

        for (var i = 0; i < text.Length; i++)
        {
            if (char.IsDigit(text[i])) count++;
        }

        if (count == 0) return Array.Empty<int>();

        var result = new int[count];

        var index = 0;

        for (var i = 0; i < text.Length; i++)
        {
            if (char.IsDigit(text[i]))
            {
                result[index] = text[i].ToDeltaInt();
                index++;
            }
        }

        return result;
    }

    public async Task<CalculationResultModel> Calculate(string? text, CancellationToken cancellationToken = default) =>
        await Task.Run<CalculationResultModel>(() =>
            {
                if (text is not { Length: > 0 })
                    return new(Result: string.Empty, Steps: Array.Empty<CalculationStepModel>());

                var numbers = ToFilteredDigitCollection(text);

                if (numbers.Length == 0)
                    return new(Result: string.Empty, Steps: Array.Empty<CalculationStepModel>());

                string result;
                var steps = new List<CalculationStepModel>
                {
                    new(
                        Equation: _numerologyUiService.ComposeCalculatorEntryEquation(numbers),
                        Sum: result = numbers.ToSumString(),
                        NumberOfCharacters: numbers.Length,
                        Sequence: _numerologyUiService.ComposeCalculatorEntrySequence(numbers)
                    )
                };

                while (result.Length > 1)
                {
                    numbers = result.ToDeltaIntCollectionSequence();

                    steps.Add(
                        new(
                            Equation: _numerologyUiService.ComposeCalculatorEntryEquation(numbers),
                            Sum: result = numbers.ToSumString(),
                            NumberOfCharacters: numbers.Length,
                            Sequence: _numerologyUiService.ComposeCalculatorEntrySequence(numbers)
                        )
                    );
                }

                return new(Result: result, Steps: steps.ToArray());
            },
            cancellationToken
        );
}
