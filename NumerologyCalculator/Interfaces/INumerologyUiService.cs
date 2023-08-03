namespace NumerologyCalculator.Interfaces;

public interface INumerologyUiService
{
     string ComposeCalculatorEntryEquation<T>(IEnumerable<T> collection);

     string ComposeCalculatorEntrySequence<T>(IEnumerable<T> collection);

     string ComposeCalculatorEquationCombinedItem<TLeft, TRight>(TLeft left, TRight right);

     Task InputDelay(CancellationToken cancellationToken = default);

     string NormalizeTextInput(string? value) ;
}