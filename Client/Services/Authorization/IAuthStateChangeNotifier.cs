using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Services.Authorization;

public interface IAuthStateChangeNotifier
{
    public void NotifyUserAuthentication(Dto.Authorization authorization);
    public void NotifyUserLogout();
}