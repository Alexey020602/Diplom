using DataBase.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Diploma;

public class IdentitySeed(UserManager<IdentityUser<Guid>> userManager, RoleManager<IdentityRole<Guid>> roleManager)
{
    private readonly UserManager<IdentityUser<Guid>> userManager = userManager;
    private readonly RoleManager<IdentityRole<Guid>> roleManager = roleManager;
    private const string AdminName = "Admin";
    private const string AdminPassword = "123456";

    public async Task Seed()
    {
        await AddAdmin();
        await SeedRoles();
        await AddAdminToRolesAsync();
    }
    private async Task AddAdmin()
    {
        var user = await userManager.FindByNameAsync(AdminName);
        if (user is not null)
            return;

        var result = await userManager.CreateAsync(
            new()
            {
                UserName = AdminName
            },
            AdminPassword
        );

        if (result.Succeeded)
            return;

        throw new InvalidOperationException(result.Errors.ToString());
    }

    private async Task AddAdminToRolesAsync()
    {
        var user = await userManager.FindByNameAsync(AdminName);

        if (user is null)
            throw new InvalidOperationException();
        
        var result = await userManager.AddToRolesAsync(
            user,
            new[]
            {
                RoleDefaults.Admin,
                RoleDefaults.Ctt,
                RoleDefaults.Cip
            }
            );
    }
    
    private async Task SeedRoles()
    {
        await AddRole(RoleDefaults.Admin);
        await AddRole(RoleDefaults.Ctt);
        await AddRole(RoleDefaults.Cip);
    }
    private async Task AddRole(string name)
    {
        if (await roleManager.RoleExistsAsync(name)) 
            return;

        var result = await roleManager.CreateAsync(new()
        {
            Name = name,
        });
        
        if (result.Succeeded) 
            return;

        throw new InvalidOperationException(result.Errors.ToString());
    }
}