﻿using NumerologyCalculator.Interfaces;
using NumerologyCalculator.Models;

namespace NumerologyCalculator.Services;

public class NumerologyUiService : INumerologyUiService
{
    private readonly NumerologyUiConfig _config;

    public NumerologyUiService(NumerologyUiConfig config) =>
        _config = config;

    public string ComposeCalculatorEntryEquation<T>(T[] collection) =>
        string.Join(_config.CalculatorEquationSeparator, collection);

    public string ComposeCalculatorEntrySequence<T>(T[] collection) =>
        string.Join(string.Empty, collection);

    public string ComposeCalculatorEquationCombinedItem<TLeft, TRight>(TLeft left, TRight right) =>
        string.Format(_config.CalculatorEquationCombinedItemTemplate, left, right);

    public async Task InputDelay(CancellationToken cancellationToken = default) =>
        await Task.Delay(_config.UiInputDelay, cancellationToken);

    public string NormalizeTextInput(string? value) => value?.Replace(" ", string.Empty) switch
    {
        { Length: > 0 } v when v.Length > _config.MaxInputChars => v[.._config.MaxInputChars],
        { Length: > 0 } v => v,
        _ => string.Empty
    };
}
