namespace NumerologyCalculator.Models;

public record CalculationResult(string Result, IEnumerable<CalculationStep> Steps);