namespace NumerologyCalculator.Services;

public class NumerologyLetterCalculatorService
{
    private const int _charCodeDelta = 48;
    private readonly NumerologyUiService _numerologyUiService;
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

    public NumerologyLetterCalculatorService(NumerologyUiService numerologyUiService) =>
        _numerologyUiService = numerologyUiService;

    public async Task<(string result, IEnumerable<(string equation, string sum, int numberOfLetters, string sequence)> steps)> Calculate(string text, CancellationToken cancellationToken) =>
        await Task.Run(() =>
            {
                try
                {
                    var steps = new List<(string equation, string sum, int numberOfLetters, string sequence)>();
                    var workingCollection =
                        text.ToUpper()
                            .Where(x => char.IsAsciiLetter(x) && _map.ContainsKey(x))
                            .Select(letter => (letter, number: _map[letter], composed: $"({letter}: {_map[letter]})"))
                            .ToList();

                    if (workingCollection is { Count: 0 })
                        return (result: string.Empty, steps);

                    var result = workingCollection.Select(x => x.number).Sum().ToString();

                    steps.Add(
                        (
                            equation: _numerologyUiService.ComposeCalculatorEntryEquation(workingCollection.Select(x => x.composed)),
                            sum: result,
                            numberOfLetters: workingCollection.Count,
                            sequence: _numerologyUiService.ComposeCalculatorEntrySequence(workingCollection.Select(x => x.number))
                        )
                    );

                    var numberCollection = new List<int>();

                    while (result is { Length: > 1 })
                    {
                        numberCollection = result.Select(x => (int)x - _charCodeDelta).ToList();
                        result = numberCollection.Sum().ToString();

                        steps.Add(
                            (
                                equation: _numerologyUiService.ComposeCalculatorEntryEquation(numberCollection),
                                sum: result,
                                numberOfLetters: workingCollection.Count,
                                sequence: _numerologyUiService.ComposeCalculatorEntrySequence(numberCollection)
                            )
                        );
                    }

                    return (result, steps);
                }
                catch
                {
                    return (
                        result: string.Empty,
                        steps: Enumerable.Empty<(string equation, string sum, int numberOfLetters, string sequence)>()
                    );
                }
            },
            cancellationToken
        );
}