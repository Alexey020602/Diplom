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
        services.AddTransient<IPartnersForAgreementService, PartnersForAgreementService>();
        services.AddTransient<IDivisionsForAgreementService, DivisionsForAgreementService>();
        services.AddRefitClient<IPartnersService>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("partners"));
        services.AddTransient<IReadApi<PartnerShort>>(p => p.GetRequiredService<IPartnersService>());

        services.AddRefitClient<IPartnerTypesService>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("partnerTypes"));

        services.AddRefitClient<IDirectionsService>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("directions"));

        services.AddTransient<IReadApi<Model.Direction>>(p => p.GetRequiredService<IDirectionsService>());
        //services.AddTransient<IReadApi<Model.Divisions.>>()
        services.AddRefitClient<IDivisionsService>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("divisions"));
        services.AddTransient<IReadApi<DivisionShort>>(p => p.GetRequiredService<IDivisionsService>());

        services.AddRefitClient<IReadApi<Model.Divisions.Faculty>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("faculties"));

        services.AddRefitClient<IReadApi<AgreementType>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementTypes"));

        services.AddRefitClient<IReadApi<AgreementStatus>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementStatuses"));

        services.AddRefitClient<IReadApi<Model.Interactions.Type>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("interactiontypes"));

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
