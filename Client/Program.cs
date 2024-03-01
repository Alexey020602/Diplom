using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Services;
using Refit;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var startup = new Startup(builder.HostEnvironment.BaseAddress);
startup.ConfigureServices(builder.Services);

builder.RootComponents.Add<RoutingComponent>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
