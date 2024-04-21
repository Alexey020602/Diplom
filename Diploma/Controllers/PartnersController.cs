using Microsoft.AspNetCore.Mvc;
using Model.Partners;
using Diploma.Extensions.ModelToDao;
using Diploma.Services;
using Model.Agreements;
using Model.Interactions;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartnersController(IPartnersRepository partnersRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ShowPartners([FromQuery] int? partnerTypeId, [FromQuery] int? directionId)
    {
        var partners = (await partnersRepository.GetPartnersAsync(partnerTypeId, directionId)).Select(p => new
        {
            Name = p.ShortName,
            p.Id,
        } ) ;

        return new JsonResult(partners);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ShowPartnerById(int id)
    {
        var partner = (await partnersRepository.GetPartnerByIdAsync(id)).ConvertToModel();
        if (partner == null)
        {
            return NotFound("Нет соответсвующего партнера");
        }
        else
        {
            return new JsonResult(partner);
        }
    }

    [HttpGet("{id:int}/agreements")]
    public async Task<IActionResult> GetAgreementsForPartner(int id)
    {
        return new JsonResult((await partnersRepository.GetAgreementsForPartnerWithId(id)).Select(a => new AgreementShort(a.Id, a.ToString())));
    }

    [HttpGet("{id:int}/interactions")]
    public async Task<IActionResult> GetInteractionsForPartner(int id) => new JsonResult(
        (await partnersRepository.GetInteractionsForPartnerWithId(id)).Select(i => new InteractionShort(i.Id, i.ToString()))
        );

    [HttpPost]
    public async Task<IActionResult> AddPartner([FromBody] Model.Partners.Partner partner)
    {
        await partnersRepository.AddPartnerAsync(partner.ConvertToDao());
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartner(int id, [FromBody] Model.Partners.Partner partner)
    {
        await partnersRepository.UpdatePartnerAsync(id, partner.ConvertToDao());
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task DeletePartner(int id)
    {
        await partnersRepository.DeletePartnerByIdAsync(id);
    }
}

