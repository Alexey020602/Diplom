using System.Text.Json.Serialization;

namespace Model.Identity;

public class AuthResponse
{
    public string Login { get; set; }
    public List<Role> Roles { get; set; }
    public string Token { get; set; }
}