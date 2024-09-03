using Blazored.LocalStorage;
using Client.AuthProviders;
using Client.Dto;
using Client.Services.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Model.Identity;

namespace Client.Features;

public class AuthenticationService(IAuthApi authApi, ILocalStorageService localStorage, NotifiedAuthStateProvider stateProvider): IAuthenticationService
{
    private readonly IAuthApi authApi = authApi;
    private readonly ILocalStorageService localStorage = localStorage;
    private readonly NotifiedAuthStateProvider stateProvider = stateProvider;
    
    public Task RegisterUser(RegistrationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task Login(AuthRequest request)
    {
        var result = await authApi.Login(request);
        await localStorage.SetItemAsync("authorization", new Authorization
        {
            Roles = result.Roles.Select(r => r.Name).ToList(),
            Token = result.Token,
        });
        stateProvider.NotifyUserAuthentication(result.Login);
        
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}