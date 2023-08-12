namespace NumerologyCalculator.Models;

[ExcludeFromCodeCoverage]
public record CalculationResultModel(string Result, CalculationStepModel[] Steps);
