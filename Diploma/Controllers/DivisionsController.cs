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
    public async Task<IActionResult> GetDivisions() => new JsonResult( (await repository.GetDivisions()).Select(d => new DivisionShort(d.Id, d.ShortName)));
}