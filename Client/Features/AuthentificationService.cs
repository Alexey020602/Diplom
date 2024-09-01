using Blazored.LocalStorage;
using Client.Services.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Model.Identity;

namespace Client.Features;

public class AuthenticationService(IAuthApi authApi, ILocalStorageService localStorage, AuthenticationStateProvider stateProvider): IAuthenticationService
{
    private readonly IAuthApi authApi = authApi;
    private readonly ILocalStorageService localStorage = localStorage;
    private readonly AuthenticationStateProvider stateProvider = stateProvider;
    
    public Task RegisterUser(RegistrationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task Login(AuthRequest request)
    {
        var result = await authApi.Login(request);
        await localStorage.SetItemAsync("authToken", result.Token);
        
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}