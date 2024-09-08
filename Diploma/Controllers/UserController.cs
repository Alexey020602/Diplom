using DataBase.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Identity;
using Role = Model.Identity.Role;

namespace Diploma.Controllers;

[ApiController]
public class UserController(
    UserManager<IdentityUser<Guid>> userManager,
    ITokenService tokenService) : ControllerBase
{
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

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var managedUser = await userManager.FindByNameAsync(request.Login!);

        if (managedUser == null) return BadRequest("Bad credentials");

        var roles = await userManager.GetRolesAsync(managedUser);

        var isPasswordValid = await userManager.CheckPasswordAsync(managedUser, request.Password!);

        if (!isPasswordValid) return BadRequest("Bad credentials");

        return Ok(
            new AuthResponse
            {
                Login = managedUser.UserName!,
                Token = tokenService.CreateToken(managedUser, roles),
                Roles = roles.Select(role => new Role(role)).ToList()
            }
        );
    }
}