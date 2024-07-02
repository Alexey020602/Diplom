using Diploma.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgreementStatusesController(IAgreementStatusRepository repository): ControllerBase
{
    [HttpGet] public async Task<IActionResult> Get() => new JsonResult( await repository.GetAgreementStatuses() );
}
