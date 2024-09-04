using Blazored.LocalStorage;
using Client.AuthProviders;
using Client.Features;
using Client.Network;
using Client.Services;
using Client.Services.Api;
using Client.Services.Api.BaseApi;
using Client.Services.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Model.Agreements;
using Model.Divisions;
using Model.Identity;
using Model.Partners;
using Radzen;
using Refit;
using AgreementStatus = Model.Agreements.Status;

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
        
        
        services.AddTransient<ITokenStorage, AuthorizationStorage>();
        services.AddTransient<DelegatingHandler, MyDelegatingHandler>();
        services.AddTransient<IPartnersForAgreementService, PartnersForAgreementService>();
        services.AddTransient<IDivisionsForAgreementService, DivisionsForAgreementService>();
        services.AddRefitClient<IPartnersService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("partners"));
        services.AddTransient<IReadApi<PartnerShort>>(p => p.GetRequiredService<IPartnersService>());
        

        services.AddRefitClient<IPartnerTypesService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("partnerTypes"))
            .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>());

        services.AddRefitClient<IDirectionsService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("directions"));

        services.AddTransient<IReadApi<Model.Direction>>(p => p.GetRequiredService<IDirectionsService>());
        //services.AddTransient<IReadApi<Model.Divisions.>>()
        services.AddRefitClient<IDivisionsService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("divisions"));
        services.AddTransient<IReadApi<DivisionShort>>(p => p.GetRequiredService<IDivisionsService>());

        services.AddRefitClient<IReadApi<Model.Divisions.Faculty>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("faculties"));

        services.AddRefitClient<IReadApi<Model.Agreements.AgreementType>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementTypes"));

        services.AddRefitClient<IReadApi<AgreementStatus>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementStatuses"));

        services.AddRefitClient<IReadApi<Model.Interactions.InteractionType>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("interactiontypes"));

        services.AddRefitClient<IReadApi<PartnerType>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("partnerTypes"));
        services.AddRefitClient<IAgreementService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreements"));

        services.AddRefitClient<IInteractionsService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("interactions"));

        services.AddRefitClient<IAuthApi>()
            .ConfigureHttpClient((client) => client.BaseAddress = new Uri(baseAddress));
        services.AddTransient<IAuthenticationService, AuthenticationService>();

        services.AddRefitClient<IReadApi<Role>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("roles"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddScoped<NotifiedAuthStateProvider, AuthStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<NotifiedAuthStateProvider>());
        
        services.AddBlazoredLocalStorage();
        services.AddRadzenComponents();
    }
    private string ApiAddress => baseAddress + "api/";
    //private Uri ApiBaseAddress => new(ApiAddress, UriKind.Absolute);
    private Uri CreateApiUrlWithPath(string path) => new(ApiAddress + path, UriKind.Absolute);
    private Action<HttpClient> ConfigureHttpClientForPath(string path) => (client) => client.BaseAddress = CreateApiUrlWithPath(path);
}
