using Diploma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModel;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgreementsController(IAgreementRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int? agreementTypeId, int? agreementStatusId) =>
        new JsonResult(
            (await repository.GetAgreements(agreementTypeId, agreementStatusId))
            .Select(agreement =>  new ListItem(agreement.Id, agreement.ToString())) 
            );
}
