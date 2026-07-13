using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Perdin.BlazorClient;
using Microsoft.AspNetCore.Components.Authorization;
using Perdin.BlazorClient.Services;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string backendApiUrl = builder.Configuration["BackendApiUrl"] ?? throw new InvalidOperationException("BackendApiUrl is not configured.");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendApiUrl) });

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<BusinessTripRequestService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<ProvinceService>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
