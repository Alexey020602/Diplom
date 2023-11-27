using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
new HttpClient
{
    BaseAddress=new Uri(builder.HostEnvironment.BaseAddress)
});

//builder.RootComponents.Add<Component>("#app");

var app = builder.Build();
await app.RunAsync();
