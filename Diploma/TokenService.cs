using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataBase.Models.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Diploma;

public interface ITokenService
{
    public string CreateToken(User user);
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

    public string CreateToken(User user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtSecurityToken(CreateClaims(user), CreateSigningCredentials(), expiration);
        var tokenHandler = new JwtSecurityTokenHandler();
        logger.LogInformation("JWT Token created");
        return tokenHandler.WriteToken(token);
    }
    
    private JwtSecurityToken CreateJwtSecurityToken(List<Claim> claims, SigningCredentials signingCredentials,
        DateTime expiration) => new(
        configuration.GetSection(SectionName)[ValidIssuer],
        configuration.GetSection(SectionName)[ValidAudience],
        claims,
        expires: expiration,
        signingCredentials: signingCredentials);
    
    private List<Claim> CreateClaims(User user) => new()
    {
        new(ClaimTypes.NameIdentifier, user.Id),
        new(ClaimTypes.Name, user.UserName!),
        new(ClaimTypes.Role, user.Role.ToString())
    };
    private SigningCredentials CreateSigningCredentials() => 
        new( 
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetSection(SectionName)[SymmetricSecurityKey])
                ), 
            SecurityAlgorithms.HmacSha256
            );
}