using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Client.Features;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.AuthProviders;

public class AuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient): NotifiedAuthStateProvider
{
    private static AuthenticationState AnonymousState => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    private readonly ILocalStorageService localStorage = localStorage;
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await GetStoredToken();
        if (string.IsNullOrWhiteSpace(token))
            return AnonymousState;

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseFromJwt(token), "jwtAuthType")));
    }

    private ValueTask<string?> GetStoredToken() => localStorage.GetItemAsync<string>("authToken");

    public override void NotifyUserAuthentication(string login)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, login)}, "jwtAuthType"));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
    }

    public override void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(AnonymousState));
    }
}