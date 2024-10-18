using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Agreements;

namespace Diploma.Controllers;

public class AgreementsController(IAgreementRepository repository) : ApiControllerBase
{
    [Authorize(Roles = "Cip")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] AgreementsFilter filter)
    {
        return new JsonResult(await repository.GetAgreements(filter));
    }

    [Authorize(Roles = "Cip")]
    [HttpGet("count")]
    public Task<int> GetCount() => repository.CountAgreements();
    
    [Authorize(Roles = "Cip")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        return new JsonResult(await repository.GetAgreementById(id));
    }

    
    [Authorize(Roles = "Ctt")]
    [HttpPost]
    public async Task<IActionResult> Add(Agreement agreement)
    {
        await repository.AddAgreement(agreement);

        return NoContent();
    }

    [Authorize(Roles = "Ctt")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Agreement agreement)
    {
        try
        {
            await repository.UpdateAgreement(id, agreement);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

        return NoContent();
    }

    [Authorize(Roles = "Ctt")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await repository.DeleteAgreement(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

        return NoContent();
    }
}