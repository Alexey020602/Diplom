using System.ComponentModel.DataAnnotations;

namespace Model.Identity;

public class RegistrationResponse
{
    [Required] public string? Login { get; set; }
    public IEnumerable<Role> Roles { get; set; } = default!;
}