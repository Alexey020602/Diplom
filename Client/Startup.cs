using Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

namespace Client;

public class Startup(string baseAddress)
{
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddScoped(sp => new HttpClient
        //{
        //    BaseAddress = ApiBaseAddress,
        //}); ;
        services.AddRefitClient<IPartnersService>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("partners"));

        services.AddRefitClient<IPartnerTypesService>()
        .ConfigureHttpClient(ConfigureHttpClientForPath("partnerTypes"));

        services.AddRefitClient<IDirectionsService>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("directions"));
    }
    private string ApiAddress => baseAddress + "api/";
    private Uri ApiBaseAddress => new(ApiAddress, UriKind.Absolute);
    private Uri CreateApiUrlWithPath(string path) => new(ApiAddress + path, UriKind.Absolute);

    private Action<HttpClient> ConfigureHttpClientForPath(string path) => (client) => client.BaseAddress = CreateApiUrlWithPath(path);
}
