using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataBase.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Diploma;

public interface ITokenService
{
    public string CreateToken(IdentityUser<Guid> user, IEnumerable<string> roles);
}
public class TokenService(ILogger<TokenService> logger, IConfiguration configuration): ITokenService
{
    private const int ExpirationMinutes = 60;
    private const string SectionName = "JwtTokenSettings";
    private const string ValidIssuer = "ValidIssuer";
    private const string ValidAudience = "ValidAudience";
    private const string SymmetricSecurityKey = "SymmetricSecurityKey";
    private readonly ILogger<TokenService> logger = logger;
    private readonly IConfiguration configuration = configuration;

    public string CreateToken(IdentityUser<Guid> user, IEnumerable<string> roles)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtSecurityToken(CreateClaims(user, roles), CreateSigningCredentials(), expiration);
        var tokenHandler = new JwtSecurityTokenHandler();
        logger.LogInformation("JWT Token created");
        return tokenHandler.WriteToken(token);
    }
    
    private JwtSecurityToken CreateJwtSecurityToken(IEnumerable<Claim> claims, SigningCredentials signingCredentials,
        DateTime expiration) => new(
        configuration.GetSection(SectionName)[ValidIssuer],
        configuration.GetSection(SectionName)[ValidAudience],
        claims,
        expires: expiration,
        signingCredentials: signingCredentials);
    
    private IEnumerable<Claim> CreateClaims(IdentityUser<Guid> user, IEnumerable<string> roles) => Enumerable.Concat(
        CreateUserClaims(user),
        CreateRoleClaims(roles)
        );

    private IEnumerable<Claim> CreateUserClaims(IdentityUser<Guid> user) => new[] 
    { 
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName!),
        
    };

    private IEnumerable<Claim> CreateRoleClaims(IEnumerable<string> roles) =>
        roles.Select(role => new Claim(ClaimTypes.Role, role));
    private SigningCredentials CreateSigningCredentials() => 
        new( 
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetSection(SectionName)[SymmetricSecurityKey])
                ), 
            SecurityAlgorithms.HmacSha256
            );
}