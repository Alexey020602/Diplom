using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Console.WriteLine("Begin");
var baseAddress = builder.HostEnvironment.BaseAddress + "api/";
Console.WriteLine(baseAddress);
builder.Services.AddScoped(sp =>
new HttpClient
{
    BaseAddress=new Uri(baseAddress)
});

builder.Services.AddTransient<IPartnersService, PartnersService>();
builder.Services.AddTransient<IPartnerTypesService, PartnerTypesService>();
builder.Services.AddTransient<IDirectionsService, DirectionsService>();

builder.RootComponents.Add<App>("#app");

var app = builder.Build();

Console.WriteLine("App run");
await app.RunAsync();
