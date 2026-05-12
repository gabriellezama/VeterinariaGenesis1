using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VeterinariaGenesis.Client;

using VeterinariaGenesis.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp =>
{
    var http = new HttpClient { BaseAddress = new Uri("https://localhost:7085") };
    return new AppStateService(http);
});

await builder.Build().RunAsync();
