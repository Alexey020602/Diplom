using System.ComponentModel.DataAnnotations;

namespace Model.Identity;

public class RegistrationRequest
{
    [Required(ErrorMessage = "Необходимо ввести логин")] 
    public string? Login { get; set; }
    [Required(ErrorMessage = "Необходимо ввести пароль")]
    [MinLength(6, ErrorMessage = "Пароль должен быть длинной не менее 6 сиволов")]
    public string? Password { get; set; }

    [MinLength(1, ErrorMessage = "Необходимо выбрать хотя бы одну роль")]
    public IEnumerable<Role> Roles { get; set; } = [];
}