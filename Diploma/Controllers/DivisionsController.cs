using Diploma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModel;

namespace Diploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DivisionsController(IDivisionRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDivisions(int? facultyId) => 
        new JsonResult( (await repository.GetDivisions(facultyId)).Select(d => new DivisionShort(d.Id, d.ShortName)));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDivision(int id) => new JsonResult( await repository.GetDivision(id));
}