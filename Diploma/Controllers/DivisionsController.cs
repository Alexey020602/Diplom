using Diploma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataBase.Models;
using Model.Divisions;
using Model.Extensions;

namespace Diploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DivisionsController(IDivisionRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDivisions(int? facultyId) => 
        new JsonResult( (await repository.GetDivisions(facultyId)).Select(d => new DivisionShort(d.Id, d.ShortName)));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDivision(int id) => new JsonResult( (await repository.GetDivision(id)).ToModel());

    [HttpPost]
    public async Task<IActionResult> CreateDivision(Model.Divisions.Division division)
    {
        await repository.AddDivision(division.ToDao());
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDivision(int id, Model.Divisions.Division division)
    {
        await repository.UpdateDivision(id, division.ToDao());
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public Task DeleteDivision(int id) => repository.DeleteDivision(id);
}