using System.ComponentModel.DataAnnotations;

namespace Model.Identity;

public class AuthRequest
{
    [Required(ErrorMessage = "Необходимо ввести логин")] 
    public string? Login { get; set; }
    
    [Required(ErrorMessage = "Необходимо ввести пароль")]
    public string? Password { get; set; }
}