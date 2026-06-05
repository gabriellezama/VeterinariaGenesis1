using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VeterinariaGenesis.Client;

using VeterinariaGenesis.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp =>
{
    // En producción, el Cliente y la API se sirven desde la misma URL
    var apiUri = builder.HostEnvironment.BaseAddress;
    var http = new HttpClient { BaseAddress = new Uri(apiUri) };
    return new AppStateService(http);
});

await builder.Build().RunAsync();
