using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NumerologyCalculator;
using NumerologyCalculator.Interfaces;
using NumerologyCalculator.Models;
using NumerologyCalculator.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddSingleton(new NumerologyUiConfig(
        MaxInputChars: 1_000,
        UiInputDelay: 600,
        CalculatorEquationSeparator: " + ",
        CalculatorEquationCombinedItemTemplate: "({0}: {1})"
    ))
    .AddSingleton<INumerologyUiService, NumerologyUiService>()
    .AddSingleton(new NumerologyLinksConfig(Url: "https://number.academy/numerology/{0}"))
    .AddSingleton<INumerologyLinksService, NumerologyLinksService>()
    .AddSingleton<INumerologyDigitCalculatorService, NumerologyDigitCalculatorService>()
    .AddSingleton<INumerologyLetterCalculatorService, NumerologyLetterCalculatorService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();

