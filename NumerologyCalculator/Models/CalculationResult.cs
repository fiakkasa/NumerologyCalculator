namespace NumerologyCalculator.Models;

[ExcludeFromCodeCoverage]
public record CalculationResult(string Result, IEnumerable<CalculationStep> Steps);
