using System.Security.Claims;

namespace Client.Dto;

public class Authorization
{
    public string Token { get; set; }
    public List<string> Roles { get; set; } = [];
    public string? Login { get; set; }

    public IEnumerable<Claim> Claims => RolesClaims.Concat(UserClaims);
    private IEnumerable<Claim> UserClaims => string.IsNullOrEmpty(Login) ? [] : new Claim[] { new(ClaimTypes.Name, Login)};
    private IEnumerable<Claim> RolesClaims => Roles.Select(r => new Claim(ClaimTypes.Role, r));
}