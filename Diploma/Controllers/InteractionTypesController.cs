using Diploma.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

public class InteractionTypesController(IInteractionTypeRepository repository) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return new JsonResult(await repository.GetInteractionTypes());
    }
}