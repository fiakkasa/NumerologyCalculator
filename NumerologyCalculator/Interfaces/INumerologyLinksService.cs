namespace NumerologyCalculator.Interfaces;

public interface INumerologyLinksService
{
    bool IsNumerologyLinkEligible(string? value, out string result);

    string GetNumerologyUrl(string? value);
}