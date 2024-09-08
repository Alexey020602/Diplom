using System.ComponentModel.DataAnnotations;

namespace Model.Identity;

public class RegistrationResponse
{
    [Required] public string? Login { get; set; }
    public List<Role> Roles { get; set; }
}