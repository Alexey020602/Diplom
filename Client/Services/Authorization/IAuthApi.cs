using Model.Identity;
using Refit;

namespace Client.Services.Authorization;

public interface IAuthApi
{
    [Post("/register")]
    Task<RegistrationResponse> RegisterUser(RegistrationRequest request);

    [Post("/login")]
    Task<AuthResponse> Login(AuthRequest request);

    [Post("/logout")]
    Task Logout();
}