using NumerologyCalculator.Models;

namespace NumerologyCalculator.Interfaces;

public interface INumerologyDigitCalculatorService
{
    Task<CalculationResult> Calculate(string? text, CancellationToken cancellationToken = default);
}
