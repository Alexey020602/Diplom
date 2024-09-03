using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Client.Dto;
using Client.Features;
using Microsoft.AspNetCore.Components.Authorization;
using Model.Identity;

namespace Client.AuthProviders;

public class AuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient): NotifiedAuthStateProvider
{
    private static AuthenticationState AnonymousState => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    private readonly ILocalStorageService localStorage = localStorage;
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authorization = await GetUserAuthorization();
        if (authorization is null)
            return AnonymousState;

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authorization.Token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(
            authorization.Roles.Select(role => new Claim(ClaimTypes.Role, role)), 
            "jwtAuthType"
            )));
    }

    // private ValueTask<List<string>?> GetStoredRoles() => localStorage.GetItemAsync<List<string>>("roles");
    // private ValueTask<string?> GetStoredToken() => localStorage.GetItemAsync<string>("authToken");

    private ValueTask<Authorization?> GetUserAuthorization() =>
        localStorage.GetItemAsync<Authorization>("authorization");
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