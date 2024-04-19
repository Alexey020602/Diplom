using Diploma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InteractionTypesController(IInteractionTypeRepository repository) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> Get() => new JsonResult( await repository.GetInteractionTypes() );
}
