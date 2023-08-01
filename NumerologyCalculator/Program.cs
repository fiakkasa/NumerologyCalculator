using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NumerologyCalculator;
using NumerologyCalculator.Models;
using NumerologyCalculator.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddSingleton(new NumerologyUiConfig
    {
        MaxInputChars = 1_000,
        UiInputDelay = 600,
        CalculatorEquationSeparator = " + ",
        CalculatorEquationCombinedItemTemplate = "({0}: {1})",
        CalculatorStepEntryContainerClass = "mt-2"
    })
    .AddSingleton<NumerologyUiService>()
    .AddSingleton(new NumerologyLinksConfig
    {
        Url = "https://number.academy/numerology/{0}"
    })
    .AddSingleton<NumerologyLinksService>()
    .AddSingleton<NumerologyDigitCalculatorService>()
    .AddSingleton<NumerologyLetterCalculatorService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();

