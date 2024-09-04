using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Client.Dto;

public record class Authorization([Required] string Token, string Login, [MinLength(1)] List<string> Roles)
{
    // public string Token { get; set; } = token;
    // public List<string> Roles { get; set; } = roles;
    // public string Login { get; set; } = login;

    public IEnumerable<Claim> Claims => RolesClaims.Concat(UserClaims);
    private IEnumerable<Claim> UserClaims => string.IsNullOrEmpty(Login) ? [] : new Claim[] { new(ClaimTypes.Name, Login)};
    private IEnumerable<Claim> RolesClaims => Roles.Select(r => new Claim(ClaimTypes.Role, r));

    public override string ToString() =>
        $"""
        Login: {Login}
        Roles: {Roles}
        Token: {Token}
        """;
}