using NumerologyCalculator.Interfaces;
using NumerologyCalculator.Models;

namespace NumerologyCalculator.Services;

public class NumerologyLinksService : INumerologyLinksService
{
    private readonly NumerologyLinksConfig _config;

    public NumerologyLinksService(NumerologyLinksConfig config) =>
        _config = config;

    public bool IsNumerologyLinkEligible(string? value, out string result) =>
        (result = value is { } v ? v : string.Empty) is { Length: > 0 and <= 3 };

    public string GetNumerologyUrl(string value) =>
        string.Format(_config.Url, value);
}
