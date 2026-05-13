using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VeterinariaGenesis.Client;

using VeterinariaGenesis.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp =>
{
    var http = new HttpClient { BaseAddress = new Uri("https://veterinariagenesis1-production-3fc1.up.railway.app/") };
    return new AppStateService(http);
});

await builder.Build().RunAsync();
