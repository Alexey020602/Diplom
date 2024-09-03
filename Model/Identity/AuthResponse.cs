using System.Text.Json.Serialization;

namespace Model.Identity;

public class AuthResponse
{
    public string Login { get; set; }
    public List<Role> Roles { get; set; }
    [JsonPropertyName("access_token")] public string Token { get; set; }
}