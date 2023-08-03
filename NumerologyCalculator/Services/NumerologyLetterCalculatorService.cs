using NumerologyCalculator.Models;
using NumerologyCalculator.Interfaces;

namespace NumerologyCalculator.Services;

public class NumerologyLetterCalculatorService : INumerologyLetterCalculatorService
{
    private const int _charCodeDelta = 48;
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

    public async Task<CalculationResult> Calculate(string text, CancellationToken cancellationToken) =>
        await Task.Run<CalculationResult>(() =>
            {
                var steps = new List<CalculationStep>();
                var workingCollection =
                    text.ToUpper()
                        .Where(x => char.IsAsciiLetter(x) && _map.ContainsKey(x))
                        .Select(letter => (letter, number: _map[letter], composed: _numerologyUiService.ComposeCalculatorEquationCombinedItem(letter, _map[letter])))
                        .ToList();

                if (workingCollection is { Count: 0 })
                    return new(Result: string.Empty, Steps: steps);

                var result = workingCollection.Select(x => x.number).Sum().ToString();

                steps.Add(
                    new(
                        Equation: _numerologyUiService.ComposeCalculatorEntryEquation(workingCollection.Select(x => x.composed)),
                        Sum: result,
                        NumberOfCharacters: workingCollection.Count,
                        Sequence: _numerologyUiService.ComposeCalculatorEntrySequence(workingCollection.Select(x => x.number))
                    )
                );

                var numberCollection = new List<int>();

                while (result is { Length: > 1 })
                {
                    numberCollection = result.Select(x => x - _charCodeDelta).ToList();
                    result = numberCollection.Sum().ToString();

                    steps.Add(
                        new(
                            Equation: _numerologyUiService.ComposeCalculatorEntryEquation(numberCollection),
                            Sum: result,
                            NumberOfCharacters: workingCollection.Count,
                            Sequence: _numerologyUiService.ComposeCalculatorEntrySequence(numberCollection)
                        )
                    );
                }

                return new(Result: result, Steps: steps);
            },
            cancellationToken
        );
}
