using Blazored.LocalStorage;
using Client.AuthProviders;
using Client.Features;
using Client.Network;
using Client.Services.Api;
using Client.Services.Api.BaseApi;
using Client.Services.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Model;
using Model.Agreements;
using Model.Divisions;
using Model.Identity;
using Model.Partners;
using Radzen;
using Refit;
using AgreementStatus = Model.Agreements.Status;
using InteractionType = Model.Interactions.InteractionType;

namespace Client;

public class Startup(string baseAddress)
{
    private string ApiAddress => baseAddress + "api/";

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
            .ConfigureHttpClient(ConfigureHttpClientForPath("partners"))
            .AddHttpMessageHandler<DelegatingHandler>();
        services.AddTransient<IReadApi<PartnerShort>>(p => p.GetRequiredService<IPartnersService>());


        services.AddRefitClient<IPartnerTypesService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("partnerTypes"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddRefitClient<IDirectionsService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("directions"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddTransient<IReadApi<Direction>>(p => p.GetRequiredService<IDirectionsService>());
        //services.AddTransient<IReadApi<Model.Divisions.>>()
        services.AddRefitClient<IDivisionsService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("divisions"))
            .AddHttpMessageHandler<DelegatingHandler>();
        services.AddTransient<IReadApi<DivisionShort>>(p => p.GetRequiredService<IDivisionsService>());

        services.AddRefitClient<IReadApi<Faculty>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("faculties"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddRefitClient<IReadApi<AgreementType>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementTypes"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddRefitClient<IReadApi<AgreementStatus>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreementStatuses"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddRefitClient<IReadApi<InteractionType>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("interactiontypes"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddRefitClient<IReadApi<PartnerType>>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("partnerTypes"))
            .AddHttpMessageHandler<DelegatingHandler>();
        services.AddRefitClient<IAgreementService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("agreements"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddRefitClient<IInteractionsService>(settings)
            .ConfigureHttpClient(ConfigureHttpClientForPath("interactions"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddRefitClient<IAuthApi>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(baseAddress));
        services.AddTransient<IAuthenticationService, AuthenticationService>();

        services.AddRefitClient<IReadApi<Role>>()
            .ConfigureHttpClient(ConfigureHttpClientForPath("roles"))
            .AddHttpMessageHandler<DelegatingHandler>();

        services.AddScoped<NotifiedAuthStateProvider, AuthStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<NotifiedAuthStateProvider>());

        services.AddBlazoredLocalStorage();
        services.AddRadzenComponents();
    }

    private Uri CreateApiUrlWithPath(string path)
    {
        return new Uri(ApiAddress + path, UriKind.Absolute);
    }

    private Action<HttpClient> ConfigureHttpClientForPath(string path)
    {
        return client => client.BaseAddress = CreateApiUrlWithPath(path);
    }
}