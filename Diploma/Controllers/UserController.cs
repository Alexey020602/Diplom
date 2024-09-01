using DataBase.Data;
using DataBase.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Identity;

namespace Diploma.Controllers;

[ApiController]
public class UserController(UserManager<User> userManager, ApplicationContext context, ITokenService tokenService) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var creationResult = await userManager.CreateAsync(
            new User
            {
                UserName = request.Login,
                Role = request.Role,
            },
            request.Password!
        );

        if (creationResult.Succeeded)
        {
            return CreatedAtAction(nameof(Register), new RegistrationResponse
            {
                Login = request.Login,
                Role = request.Role
            });
        }
        
        foreach (var error in creationResult.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
    }
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await userManager.FindByNameAsync(request.Login!);

        if (managedUser == null)
        {
            return BadRequest("Bad credentials");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(managedUser, request.Password!);

        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }
        
        //TODO попробовать использовать SignInManager
        var userInDb = await context.Users.FirstOrDefaultAsync(u => u.UserName == request.Login);

        if (userInDb is null)
        {
            return Unauthorized();
        }

        return Ok(
            new AuthResponse
            {
                Login = userInDb.UserName!,
                Token = tokenService.CreateToken(userInDb)
            }
        );
    }
}