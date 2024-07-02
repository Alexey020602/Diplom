using Client.Network;
using Client.Services;
using Client.Services.BaseApi;
using Model.Divisions;
using Model.Partners;
using Refit;
using AgreementType = Model.Agreements.Type;
using AgreementStatus = Model.Agreements.Status;
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
        var settings = new RefitSettings(new NewtonsoftJsonContentSerializer());
        services.AddTransient<IPartnersForAgreementService, PartnersForAgreementService>();
        services.AddTransient<IDivisionsForAgreementService, DivisionsForAgreementService>();
        services.AddRefitClient<IPartnersService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("partners"));
        services.AddTransient<IReadApi<PartnerShort>>(p => p.GetRequiredService<IPartnersService>());

        services.AddRefitClient<IPartnerTypesService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("partnerTypes"));

        services.AddRefitClient<IDirectionsService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("directions"));

        services.AddTransient<IReadApi<Model.Direction>>(p => p.GetRequiredService<IDirectionsService>());
        //services.AddTransient<IReadApi<Model.Divisions.>>()
        services.AddRefitClient<IDivisionsService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("divisions"));
        services.AddTransient<IReadApi<DivisionShort>>(p => p.GetRequiredService<IDivisionsService>());

        services.AddRefitClient<IReadApi<Model.Divisions.Faculty>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("faculties"));

        services.AddRefitClient<IReadApi<AgreementType>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementTypes"));

        services.AddRefitClient<IReadApi<AgreementStatus>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementStatuses"));

        services.AddRefitClient<IReadApi<Model.Interactions.Type>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("interactiontypes"));

        services.AddRefitClient<IReadApi<Type>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("partnerTypes"));
        services.AddRefitClient<IAgreementService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreements"));

        services.AddRefitClient<IInteractionsService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("interactions"));
    }
    private string ApiAddress => baseAddress + "api/";
    //private Uri ApiBaseAddress => new(ApiAddress, UriKind.Absolute);
    private Uri CreateApiUrlWithPath(string path) => new(ApiAddress + path, UriKind.Absolute);

    private Action<HttpClient> ConfigureHttpClientForPath(string path) => (client) => client.BaseAddress = CreateApiUrlWithPath(path);
}
