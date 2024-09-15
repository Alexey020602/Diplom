using System.Net.Http.Headers;
using System.Security.Claims;
using Client.Dto;
using Client.Services.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.AuthProviders;

public class AuthStateProvider(ITokenStorage authorizationStorage, HttpClient httpClient) : NotifiedAuthStateProvider
{
    private const string StorageKey = "authorization";
    private readonly ITokenStorage authorizationStorage = authorizationStorage;
    private static AuthenticationState AnonymousState => new(new ClaimsPrincipal(new ClaimsIdentity()));

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authorization = await GetUserAuthorization();
        Console.WriteLine($"GetState {authorization}");
        if (authorization is null)
            return AnonymousState;

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authorization.Token);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(
            authorization.Claims,
            "jwtAuthType"
        )));
    }

    public override async Task Logout()
    {
        await authorizationStorage.RemoveAuthorization();
        NotifyUserLogout();
    }

    public override async Task Login(Authorization authorization)
    {
        await authorizationStorage.SetAuthorization(authorization);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authorization.Token);
        Console.WriteLine($"Login {authorization}");
        NotifyUserAuthentication(authorization);
    }

    private async ValueTask<Authorization?> GetUserAuthorization()
    {
        return await authorizationStorage.GetAuthorization();
    }

    private void NotifyUserAuthentication(Authorization authorization)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(authorization.Claims, "jwtAuthType"));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
    }

    private void NotifyUserLogout()
    {
        httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(Task.FromResult(AnonymousState));
    }
}