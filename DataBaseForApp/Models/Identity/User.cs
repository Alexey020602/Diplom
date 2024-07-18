using Microsoft.AspNetCore.Identity;

namespace DataBase.Models.Identity;

public class User: IdentityUser
{
    public Role Role { get; set; }
}