using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

[Route("authorization")]
public class AuthorizationController: ControllerBase
{
    [HttpPost("register")]
    public Task<IActionResult> Register()
    {
        throw new NotImplementedException();
    }

    [HttpPost("login")]
    public Task<IActionResult> Login()
    {
        throw new NotImplementedException();
    }
}