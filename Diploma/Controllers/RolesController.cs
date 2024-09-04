using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Identity;

namespace Diploma.Controllers;

[Authorize(Roles = "Admin")]
public class RolesController(RoleManager<IdentityRole<Guid>> roleManager): ApiControllerBase
{
    private readonly RoleManager<IdentityRole<Guid>> roleManager = roleManager;

    [HttpGet]
    public async Task<IActionResult> GetAvailableRoles() => Ok(await roleManager.Roles.Select(role => new Role(role.Name!)).ToListAsync());
}