using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

public class AgreementStatusesController(IAgreementStatusRepository repository) : ApiControllerBase
{
    [Authorize(Roles = "Cip")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return new JsonResult(await repository.GetAgreementStatuses());
    }
}