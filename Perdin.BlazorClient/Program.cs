using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Perdin.BlazorClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string backendApiUrl = builder.Configuration["BackendApiUrl"] ?? throw new InvalidOperationException("BackendApiUrl is not configured.");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendApiUrl) });

await builder.Build().RunAsync();
