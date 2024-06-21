using Diploma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Model.Agreements;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgreementsController(IAgreementRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int? agreementTypeId, int? agreementStatusId) =>
        new JsonResult(
            (await repository.GetAgreements(agreementTypeId, agreementStatusId))
            .Select(agreement =>  new AgreementShort(agreement.Id, agreement.ToString())) 
            );

    public async Task<IActionResult> GetOne(int id) => new JsonResult(await repository.GetAgreementById(id));

    [HttpPost] public async Task<IActionResult> Add(Agreement agreement)
    {
        try
        {
            await repository.AddAgreement(agreement);
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Agreement agreement)
    {
        try
        {
            await repository.UpdateAgreement(id, agreement);
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await repository.DeleteAgreement(id);
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }
        
        return NoContent();
    }
}
