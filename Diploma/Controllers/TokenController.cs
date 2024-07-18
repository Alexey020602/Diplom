using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using DataBase.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Diploma.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenController(IConfiguration configuration, UserManager<User> manager) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Authorize([FromForm(Name = "grant_type")] string grantType,
        [FromForm] string username, [FromForm] string password, [FromForm] int? scope = null,
        [FromForm(Name = "client_id")] string? clientId = default,
        [FromForm(Name = "client_secret")] string? clientSecret = default)
    {
        if (Request.Headers.TryGetValue("Authorization", out var authorization))
        {
            var authHeader = AuthenticationHeaderValue.Parse(authorization);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var parts = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length < 2)
                return Unauthorized("Invalid authorization header");

            clientId = parts[0];
            clientSecret = parts[1];
            Console.WriteLine(clientId);
            Console.WriteLine(clientSecret);
        }

        var validIssuer = configuration.GetValue<string>("JwtTokenSettings:ValidIssuer");
        var validAudience = configuration.GetValue<string>("JwtTokenSettings:ValidAudience");
        var symmetricSecurityKey = configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");
        if (validAudience != clientId || symmetricSecurityKey != clientSecret)
            return Unauthorized("No authorization header");
        
        var user = await manager.FindByIdAsync(username);
        if (
            user == null
            || !await manager.CheckPasswordAsync(user, password)
        )
            return BadRequest(new { errorText = "Invalid username or password." });

        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clientSecret)),
            SecurityAlgorithms.HmacSha256);
        var jwtHeader = new JwtHeader(credentials);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, username),
        };
        
        if (scope.HasValue)
        {
            // var scopes = scope.Split(new[] { ' ' }, 2,
            //     StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            // for (int i = 0; i < scopes.Length; i++)
            // {
                // var s = scopes[i];
                var scopeName = scope.Value.ToString();
                if (await manager.IsInRoleAsync(user, scopeName))
                    claims.Add(new(ClaimTypes.Role, scopeName));
                else
                    return Unauthorized("Invalid scope");
            // }
        }

        var token = new JwtSecurityToken(jwtHeader, new JwtPayload(issuer: validIssuer,
            audience: clientId,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(3000),
            claims: claims));

        return new JsonResult(new { access_token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}