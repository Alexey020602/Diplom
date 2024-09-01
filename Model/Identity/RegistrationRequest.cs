using System.ComponentModel.DataAnnotations;
using DataBase.Models.Identity;

namespace Model.Identity;

public class RegistrationRequest
{
    [Required] public string? Login { get; set; }
    [Required] public string? Password { get; set; }
    public Role Role { get; set; }
}