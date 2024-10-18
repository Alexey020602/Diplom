using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Interactions;
using Model.Partners;

namespace Diploma.Controllers;

public class PartnersController(IPartnersRepository partnersRepository) : ApiControllerBase
{
    // [Authorize(Roles = "Cip")]
    // [HttpGet("count")]
    // public Task<int> GetCount() => partnersRepository.PartnersCountAsync();
    [Authorize(Roles = "Cip")]
    [HttpGet]
    public async Task<IActionResult> ShowPartners([FromQuery] PartnersFilter partnersFilter)
    {
        return new JsonResult(await partnersRepository.GetPartnersAsync(partnersFilter));
    }

    [Authorize(Roles = "Cip")]
    [HttpGet("{id}")]
    public async Task<IActionResult> ShowPartnerById(int id)
    {
        var partner = await partnersRepository.GetPartnerByIdAsync(id);
        if (partner == null)
            return NotFound("Нет соответсвующего партнера");
        return new JsonResult(partner);
    }

    [Authorize(Roles = "Cip")]
    [HttpGet("{id:int}/agreements")]
    public async Task<IActionResult> GetAgreementsForPartner(int id)
    {
        return new JsonResult(await partnersRepository.GetAgreementsForPartnerWithId(id));
    }

    [Authorize(Roles = "Cip")]
    [HttpGet("{id:int}/interactions")]
    public async Task<IActionResult> GetInteractionsForPartner(int id)
    {
        return new JsonResult(
            (await partnersRepository.GetInteractionsForPartnerWithId(id)).Select(i =>
                new InteractionShort(i.Id, i.ToString()))
        );
    }
    
    [Authorize(Roles = "Ctt")]
    [HttpGet("{id:int}/candelete")]
    public async Task<IActionResult> CanDeleteDivision(int id) => Ok(await partnersRepository.CanDeletePartner(id));
 
    [Authorize(Roles = "Ctt")]
    [HttpPost]
    public async Task<IActionResult> AddPartner([FromBody] Partner partner)
    {
        await partnersRepository.AddPartnerAsync(partner);
        return Ok();
    }

    [Authorize(Roles = "Ctt")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartner(int id, [FromBody] Partner partner)
    {
        await partnersRepository.UpdatePartnerAsync(id, partner);
        return Ok();
    }

    [Authorize(Roles = "Ctt")]
    [HttpDelete("{id}")]
    public async Task DeletePartner(int id)
    {
        await partnersRepository.DeletePartnerByIdAsync(id);
    }
}