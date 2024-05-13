using Client.Services;
using Client.Services.BaseApi;
using DataBase.Models;
using Model.Divisions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using Type = Model.Partners.Type;

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

        services.AddTransient<IReadApi<Model.Direction>>(p => p.GetRequiredService<IDirectionsService>());
        //services.AddTransient<IReadApi<Model.Divisions.>>()
        services.AddRefitClient<IDivisionsService>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("divisions"));

        services.AddRefitClient<IReadApi<Model.Divisions.Faculty>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("faculties"));

        services.AddRefitClient<IReadApi<AgreementType>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementTypes"));

        services.AddRefitClient<IReadApi<AgreementStatus>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementStatuses"));

        services.AddRefitClient<IReadApi<Type>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("partnerTypes"));
        services.AddRefitClient<IAgreementService>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreements"));

        services.AddRefitClient<IInteractionsService>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("interactions"));
    }
    private string ApiAddress => baseAddress + "api/";
    //private Uri ApiBaseAddress => new(ApiAddress, UriKind.Absolute);
    private Uri CreateApiUrlWithPath(string path) => new(ApiAddress + path, UriKind.Absolute);

    private Action<HttpClient> ConfigureHttpClientForPath(string path) => (client) => client.BaseAddress = CreateApiUrlWithPath(path);
}
