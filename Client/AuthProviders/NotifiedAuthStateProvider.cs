using Client.Dto;
using Client.Services.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.AuthProviders;

public abstract class NotifiedAuthStateProvider: AuthenticationStateProvider, IAuthStateChangeNotifier
{
    public abstract void NotifyUserAuthentication(Authorization authorization);
    public abstract void NotifyUserLogout();
}