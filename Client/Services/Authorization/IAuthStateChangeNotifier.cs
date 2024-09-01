using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Services.Authorization;

public interface IAuthStateChangeNotifier
{
    public void NotifyUserAuthentication(string login);
    public void NotifyUserLogout();
}