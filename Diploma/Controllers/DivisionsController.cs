using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Divisions;

namespace Diploma.Controllers;

public class DivisionsController(IDivisionRepository repository) : ApiControllerBase
{
    [Authorize(Roles = "Cip")]
    [HttpGet]
    public async Task<IActionResult> GetDivisions(int? facultyId)
    {
        return new JsonResult(
            (await repository.GetDivisions(facultyId)).Select(d => new DivisionShort(d.Id, d.ShortName)));
    }

    [Authorize(Roles = "Cip")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDivision(int id)
    {
        return new JsonResult(await repository.GetDivision(id));
    }

    [Authorize(Roles = "Ctt")]
    [HttpGet("{id:int}/candelete")]
    public async Task<IActionResult> CanDeleteDivision(int id) => Ok(await repository.CanDeleteDivision(id));

    [Authorize(Roles = "Ctt")]
    [HttpPost]
    public async Task<IActionResult> CreateDivision(Division division)
    {
        await repository.AddDivision(division);
        return Ok();
    }

    [Authorize(Roles = "Ctt")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDivision(int id, Division division)
    {
        await repository.UpdateDivision(id, division);
        return Ok();
    }

    [Authorize(Roles = "Ctt")]
    [HttpDelete("{id:int}")]
    public Task DeleteDivision(int id)
    {
        return repository.DeleteDivision(id);
    }
}