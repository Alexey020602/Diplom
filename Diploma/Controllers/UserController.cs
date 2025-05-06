using DataBase.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Identity;
using Role = Model.Identity.Role;

namespace Diploma.Controllers;

[ApiController]
public class UserController(
    UserManager<IdentityUser<Guid>> userManager,
    ITokenService tokenService
) : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (
            await userManager.FindByNameAsync(request.Login!) is not { } managedUser ||
            !await userManager.CheckPasswordAsync(managedUser, request.Password!)
        )
            return BadRequest("Bad credentials");

        return Ok(
            new AuthResponse
            {
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Login = managedUser.UserName!,
                Token = tokenService.CreateToken(managedUser, await userManager.GetRolesAsync(managedUser)),
                Roles = (await userManager.GetRolesAsync(managedUser)).Select(role => new Role(role)).ToList()
            }
        );
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var creationResult = await userManager.CreateAsync(
            new IdentityUser<Guid>
            {
                UserName = request.Login
            },
            request.Password!
        );

        if (!creationResult.Succeeded)
        {
            foreach (var error in creationResult.Errors) ModelState.AddModelError(error.Code, error.Description);

            return BadRequest(ModelState);
        }

        var createdUser = await userManager.FindByNameAsync(request.Login!);

        if (createdUser is null) throw new InvalidOperationException();

        var addingToRolesResult =
            await userManager.AddToRolesAsync(createdUser, request.Roles.Select(role => role.Name));

        if (!addingToRolesResult.Succeeded) return BadRequest("Не получилось назначить роли");

        return CreatedAtAction(nameof(Register), new RegistrationResponse
        {
            Login = request.Login,
            Roles = request.Roles
        });
    }
}