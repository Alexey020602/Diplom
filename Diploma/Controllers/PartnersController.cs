using Microsoft.AspNetCore.Mvc;
using DataBase.Models;
using Diploma.Services;
using SharedModel;

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

