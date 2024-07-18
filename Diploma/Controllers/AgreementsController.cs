using Diploma.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Agreements;

namespace Diploma.Controllers;

// [Route("api/[controller]")]
// [ApiController]
public class AgreementsController(IAgreementRepository repository) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int? agreementTypeId, int? agreementStatusId) =>
        new JsonResult(
            await repository.GetAgreements(agreementTypeId, agreementStatusId)
        );

    [HttpGet("{id:int}")] public async Task<IActionResult> GetOne(int id) => new JsonResult(await repository.GetAgreementById(id));

    [HttpPost]
    public async Task<IActionResult> Add(Agreement agreement)
    {
        await repository.AddAgreement(agreement);

        return NoContent();
    }

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