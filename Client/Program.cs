using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Services;
using Refit;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Console.WriteLine("Begin");
var baseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api");
var otherBaseAddress = new Uri(baseAddress, "/api/");
Console.WriteLine(baseAddress);
Console.WriteLine(otherBaseAddress);
builder.Services.AddScoped(sp =>
new HttpClient
{
    BaseAddress = otherBaseAddress
});

builder.Services.AddTransient<IPartnersService, PartnersService>();
//builder.Services.AddTransient<IPartnerTypesService, PartnerTypesService>();
builder.Services.AddRefitClient<IPartnerTypesService>()
.ConfigureHttpClient(client => client.BaseAddress = baseAddress);
builder.Services.AddTransient<IDirectionsService, DirectionsService>();

builder.RootComponents.Add<RoutingComponent>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var app = builder.Build();
Console.WriteLine("App run");
await app.RunAsync();
