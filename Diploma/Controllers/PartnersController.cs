using Microsoft.AspNetCore.Mvc;
using Model.Partners;
using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Model.Agreements;
using Model.Extensions;
using Model.Interactions;

namespace Diploma.Controllers;


public class PartnersController(IPartnersRepository partnersRepository) : ApiControllerBase
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
        var partner = await partnersRepository.GetPartnerByIdAsync(id);
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
        return new JsonResult(await partnersRepository.GetAgreementsForPartnerWithId(id));
    }

    [HttpGet("{id:int}/interactions")]
    public async Task<IActionResult> GetInteractionsForPartner(int id) => new JsonResult(
        (await partnersRepository.GetInteractionsForPartnerWithId(id)).Select(i => new InteractionShort(i.Id, i.ToString()))
        );

    [HttpPost]
    public async Task<IActionResult> AddPartner([FromBody] Partner partner)
    {
        await partnersRepository.AddPartnerAsync(partner);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartner(int id, [FromBody] Partner partner)
    {
        await partnersRepository.UpdatePartnerAsync(id, partner);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task DeletePartner(int id)
    {
        await partnersRepository.DeletePartnerByIdAsync(id);
    }
}

