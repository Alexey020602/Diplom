using System.ComponentModel.DataAnnotations;
using DataBase.Models.Identity;

namespace Model.Identity;

public class RegistrationResponse
{
    [Required] public string? Login { get; set; }
    public List<Role> Roles { get; set; }
}