using Model.Identity;

namespace Client.Services.Authorization;

public interface IAuthenticationService
{
    Task RegisterUser(RegistrationRequest request);
    Task Login(AuthRequest request);
    Task Logout();
}