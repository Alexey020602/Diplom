using Diploma.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Divisions;
using Model.Extensions;

namespace Diploma.Controllers;

public class DivisionsController(IDivisionRepository repository) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDivisions(int? facultyId) => 
        new JsonResult( (await repository.GetDivisions(facultyId)).Select(d => new DivisionShort(d.Id, d.ShortName)));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDivision(int id) => new JsonResult( (await repository.GetDivision(id)));

    [HttpPost]
    public async Task<IActionResult> CreateDivision(Division division)
    {
        await repository.AddDivision(division);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDivision(int id, Division division)
    {
        await repository.UpdateDivision(id, division);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public Task DeleteDivision(int id) => repository.DeleteDivision(id);
}