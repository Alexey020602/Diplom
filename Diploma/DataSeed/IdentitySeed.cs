using DataBase.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Diploma.DataSeed;

public class IdentitySeed(
    UserManager<IdentityUser<Guid>> userManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    IHostEnvironment hostEnvironment): ISeed
{
    private const string AdminName = "Admin";
    private const string AdminPassword = "123456";
    private const string CttName = "Ctt";
    private const string CipName = "Cip";
    private readonly RoleManager<IdentityRole<Guid>> roleManager = roleManager;
    private readonly UserManager<IdentityUser<Guid>> userManager = userManager;
    private readonly IHostEnvironment hostEnvironment = hostEnvironment;

    public async Task Seed(CancellationToken cancellationToken)
    {
        await SeedUsers();
        await SeedRoles();
        await AddUsersToRoles();
    }

    private async Task SeedUsers()
    {
        await AddAdmin();
        if (!hostEnvironment.IsDevelopment()) return;
        await AddCipUser();
        await AddCttUser();
    }

    private async Task AddUsersToRoles()
    {
        await AddAdminToRolesAsync();
        if (!hostEnvironment.IsDevelopment()) return;
        await AddCipToRolesAsync();
        await AddCttToRolesAsync();
    }
    private Task AddAdmin() => AddUserWithNameAndPassword(AdminName, AdminPassword);

    private Task AddCipUser() => AddUserWithNameAndPassword(CipName, AdminPassword);

    private Task AddCttUser() => AddUserWithNameAndPassword(CttName, AdminPassword);

    private async Task AddUserWithNameAndPassword(string name, string password)
    {
        var user = await userManager.FindByNameAsync(name);
        if (user is not null)
            return;

        var result = await userManager.CreateAsync(
            new IdentityUser<Guid>
            {
                UserName = name
            },
            password
        );

        if (result.Succeeded)
            return;

        throw new InvalidOperationException(CreateIdentityResultsMessage(result));
    }

    private static string CreateIdentityResultsMessage(IdentityResult result)
    {
        return string.Join("\n", result.Errors.Select(err => err.Description));
    }

    private async Task AddUserToRolesAsync(string name, IEnumerable<string> roles)
    {
        var user = await userManager.FindByNameAsync(name);

        if (user is null)
            throw new InvalidOperationException();

        var result = await userManager.AddToRolesAsync(
            user,
            roles
        );

        if (result.Succeeded) return;

        // throw new InvalidOperationException(result.Errors.ToString());
    }

    private Task AddAdminToRolesAsync() => AddUserToRolesAsync(
        AdminName,
        [
            RoleDefaults.Admin,
            RoleDefaults.Ctt,
            RoleDefaults.Cip
        ]
    );

    private Task AddCipToRolesAsync() => AddUserToRolesAsync(
        CipName,
        [RoleDefaults.Cip]
    );

    private Task AddCttToRolesAsync() => AddUserToRolesAsync(
        CttName,
        [RoleDefaults.Cip, RoleDefaults.Ctt]
    );

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

        var result = await roleManager.CreateAsync(new IdentityRole<Guid>
        {
            Name = name
        });

        if (result.Succeeded)
            return;

        throw new InvalidOperationException(CreateIdentityResultsMessage(result));
    }
}

