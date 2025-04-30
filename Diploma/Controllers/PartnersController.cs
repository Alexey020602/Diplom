using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Interactions;
using Model.Partners;

namespace Diploma.Controllers;

public class PartnersController(IPartnersRepository partnersRepository) : ApiControllerBase
{
    [Authorize(Roles = "Cip")]
    [HttpGet]
    public async Task<IActionResult> ShowPartners([FromQuery] PartnersFilter partnersFilter)
        => new JsonResult(await partnersRepository
            .GetPartnersAsync(partnersFilter)
        );

    [Authorize(Roles = "Cip")]
    [HttpGet("{id}")]
    public async Task<IActionResult> ShowPartnerById(int id) =>
        new JsonResult(await partnersRepository.GetPartnerByIdAsync(id));

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
    public async Task DeletePartner(int id) => await partnersRepository.DeletePartnerByIdAsync(id);
}