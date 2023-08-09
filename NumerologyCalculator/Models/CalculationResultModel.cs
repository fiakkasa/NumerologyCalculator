namespace NumerologyCalculator.Models;

[ExcludeFromCodeCoverage]
public record CalculationResultModel(string Result, IEnumerable<CalculationStepModel> Steps);
