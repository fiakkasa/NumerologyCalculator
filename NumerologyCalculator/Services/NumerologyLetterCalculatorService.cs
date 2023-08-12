using NumerologyCalculator.Models;
using NumerologyCalculator.Interfaces;
using NumerologyCalculator.Extensions;
using System.Collections.Generic;
using System;

namespace NumerologyCalculator.Services;

public class NumerologyLetterCalculatorService : INumerologyLetterCalculatorService
{
    private readonly INumerologyUiService _numerologyUiService;
    private readonly Dictionary<char, int> _map = new()
    {
        ['A'] = 1,
        ['J'] = 1,
        ['S'] = 1,

        ['B'] = 2,
        ['K'] = 2,
        ['T'] = 2,

        ['C'] = 3,
        ['L'] = 3,
        ['U'] = 3,

        ['D'] = 4,
        ['M'] = 4,
        ['V'] = 4,

        ['E'] = 5,
        ['N'] = 5,
        ['W'] = 5,

        ['F'] = 6,
        ['O'] = 6,
        ['X'] = 6,

        ['G'] = 7,
        ['P'] = 7,
        ['Y'] = 7,

        ['H'] = 8,
        ['Q'] = 8,
        ['Z'] = 8,

        ['I'] = 9,
        ['R'] = 9
    };

    public NumerologyLetterCalculatorService(INumerologyUiService numerologyUiService) =>
        _numerologyUiService = numerologyUiService;

    private (char[] letters, int[] numbers, string[] composed) ToCollectionFromTextSequence(string text)
    {
        var count = 0;

        for (var i = 0; i < text.Length; i++)
        {
            var letter = char.ToUpper(text[i]);

            if (char.IsAsciiLetter(letter) && _map.ContainsKey(letter)) count++;
        }

        if (count == 0) return (letters: Array.Empty<char>(), numbers: Array.Empty<int>(), composed: Array.Empty<string>());

        var letters = new char[count];
        var numbers = new int[count];
        var composed = new string[count];

        var index = 0;

        for (var i = 0; i < text.Length; i++)
        {
            var letter = char.ToUpper(text[i]);

            if (char.IsAsciiLetter(letter) && _map.ContainsKey(letter))
            {
                letters[index] = letter;
                numbers[index] = _map[letter];
                composed[index] = _numerologyUiService.ComposeCalculatorEquationCombinedItem(letter, _map[letter]);

                index++;
            }
        }

        return (letters, numbers, composed);
    }

    public async Task<CalculationResultModel> Calculate(string? text, CancellationToken cancellationToken = default) =>
        await Task.Run<CalculationResultModel>(() =>
            {
                if (text is not { Length: > 0 })
                    return new(Result: string.Empty, Steps: Array.Empty<CalculationStepModel>());

                var (letters, numbers, composed) = ToCollectionFromTextSequence(text);

                if (letters.Length == 0)
                    return new(Result: string.Empty, Steps: Array.Empty<CalculationStepModel>());

                string result;
                var steps = new List<CalculationStepModel>
                {
                    new(
                        Equation: _numerologyUiService.ComposeCalculatorEntryEquation(composed),
                        Sum: result = numbers.ToSumString(),
                        NumberOfCharacters: numbers.Length,
                        Sequence: _numerologyUiService.ComposeCalculatorEntrySequence(letters)
                    )
                };

                while (result.Length > 1)
                {
                    numbers = result.ToCollectionFromNumericSequence();

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
