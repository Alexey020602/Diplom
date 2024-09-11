namespace Model.Identity;

public class AuthResponse
{
    public required string Login { get; init; }
    public required List<Role> Roles { get; init; }
    public required string Token { get; init; }
}