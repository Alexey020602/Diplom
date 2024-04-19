using Diploma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgreementTypesController(IAgreementTypeRepository repository): ControllerBase
{
    [HttpGet] public async Task<IActionResult> Get() => new JsonResult(await repository.GetAgreementTypes());
}
