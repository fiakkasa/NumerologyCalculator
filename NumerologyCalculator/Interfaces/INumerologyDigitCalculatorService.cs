using NumerologyCalculator.Models;

namespace NumerologyCalculator.Interfaces;

public interface INumerologyDigitCalculatorService
{
    Task<CalculationResultModel> Calculate(string? text, CancellationToken cancellationToken = default);
}
