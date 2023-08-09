using NumerologyCalculator.Models;

namespace NumerologyCalculator.Interfaces;

public interface INumerologyLetterCalculatorService
{
    Task<CalculationResultModel> Calculate(string? text, CancellationToken cancellationToken = default);
}
