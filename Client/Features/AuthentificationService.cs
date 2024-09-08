using Client.AuthProviders;
using Client.Dto;
using Client.Services.Authorization;
using Model.Identity;

namespace Client.Features;

public class AuthenticationService(IAuthApi authApi, NotifiedAuthStateProvider stateProvider) : IAuthenticationService
{
    private readonly IAuthApi authApi = authApi;
    private readonly NotifiedAuthStateProvider stateProvider = stateProvider;

    public async Task RegisterUser(RegistrationRequest request)
    {
        await authApi.RegisterUser(request);
    }

    public async Task Login(AuthRequest request)
    {
        var result = await authApi.Login(request);
        var authorization =
            new Authorization(result.Token, result.Login, result.Roles.Select(role => role.Name).ToList());

        await stateProvider.Login(authorization);
    }

    public async Task Logout()
    {
        await stateProvider.Logout();
    }
}