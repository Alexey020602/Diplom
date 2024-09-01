using Client.Services.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.AuthProviders;

public abstract class NotifiedAuthStateProvider: AuthenticationStateProvider, IAuthStateChangeNotifier
{
    public abstract void NotifyUserAuthentication(string login);
    public abstract void NotifyUserLogout();
}