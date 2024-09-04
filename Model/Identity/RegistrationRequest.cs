using System.ComponentModel.DataAnnotations;
using DataBase.Models.Identity;

namespace Model.Identity;

public class RegistrationRequest
{
    [Required] public string? Login { get; set; }
    [Required] public string? Password { get; set; }
    [MinLength(1, ErrorMessage = "Необходимо выбрать хотя бы одну роль")] public List<Role> Roles { get; set; }
}