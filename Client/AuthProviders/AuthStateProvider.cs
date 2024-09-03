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
    private const string StorageKey = "authorization";
    private static AuthenticationState AnonymousState => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    private readonly ILocalStorageService localStorage = localStorage;
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authorization = await GetUserAuthorization();
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
        await localStorage.RemoveItemAsync(StorageKey);
        NotifyUserLogout();
    }

    public override async Task Login(Authorization authorization)
    {
        await localStorage.SetItemAsync(StorageKey, authorization);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authorization.Token);
        NotifyUserAuthentication(authorization);
    }
    private ValueTask<Authorization?> GetUserAuthorization() =>
        localStorage.GetItemAsync<Authorization>(StorageKey);
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