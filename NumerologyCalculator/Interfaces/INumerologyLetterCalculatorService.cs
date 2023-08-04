using NumerologyCalculator.Models;

namespace NumerologyCalculator.Interfaces;

public interface INumerologyLetterCalculatorService
{
    Task<CalculationResult> Calculate(string text, CancellationToken cancellationToken = default);
}
