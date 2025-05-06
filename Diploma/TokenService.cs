using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Diploma;

public interface ITokenService
{
    public string CreateToken(IdentityUser<Guid> user, IEnumerable<string> roles);
}

public class TokenService(ILogger<TokenService> logger, IConfiguration configuration) : ITokenService
{
    private const int ExpirationMinutes = 60;
    private const string SectionName = "JwtTokenSettings";
    private const string ValidIssuer = "ValidIssuer";
    private const string ValidAudience = "ValidAudience";
    private const string SymmetricSecurityKey = "SymmetricSecurityKey";
    private readonly IConfiguration configuration = configuration;
    private readonly ILogger<TokenService> logger = logger;

    public string CreateToken(IdentityUser<Guid> user, IEnumerable<string> roles)
    {
        var token = new JwtSecurityToken(
            configuration.GetSection(SectionName)[ValidIssuer],
            configuration.GetSection(SectionName)[ValidAudience],
            CreateClaims(user, roles),
            expires: DateTime.UtcNow.AddMinutes(ExpirationMinutes),
            signingCredentials: CreateSigningCredentials());

        var tokenHandler = new JwtSecurityTokenHandler();
        logger.LogInformation("JWT Token created");
        return tokenHandler.WriteToken(token);
    }

    private static IEnumerable<Claim> CreateClaims(IdentityUser<Guid> user, IEnumerable<string> roles)
    {
        return roles.Select(role => new Claim(ClaimTypes.Role, role))
            .Concat(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName!)
                ]
            );
    }

    private SigningCredentials CreateSigningCredentials() =>
        new(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetSection(SectionName)[SymmetricSecurityKey]!)
            ),
            SecurityAlgorithms.HmacSha256
        );
}