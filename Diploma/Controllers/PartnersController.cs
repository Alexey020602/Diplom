using Microsoft.AspNetCore.Mvc;
using DataBase.Models;
using Diploma.Services;
using SharedModel;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartnersController : ControllerBase
{
    //private ApplicationContext _context { get; set; }
    private IPartnersRepository _partnersRepository;

    public PartnersController(IPartnersRepository partnersRepository)
    {
        _partnersRepository = partnersRepository;
    }

    [HttpGet]
    public async Task<IActionResult> ShowPartners()
    {
        var partners = await _partnersRepository.GetPartnersAsync();

        return new JsonResult(from p in partners select new PartnerForList(p.Id, p.ShortName));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ShowPartnerById(int id)
    {
        var partner = await _partnersRepository.GetPartnerByIdAsync(id);
        if (partner == null)
        {
            return NotFound("Нет соответсвующего партнера");
        }
        else
        {
            return new JsonResult(partner);
        }
    }

    [HttpPut]
    public async Task<IActionResult> AddPartner([FromBody] Partner partner)
    {
        await _partnersRepository.AddPartnerAsync(partner);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePartner([FromBody] Partner partner)
    {
        await _partnersRepository.UpdatePartnerAsync(partner);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task DeletePartner(int id)
    {
        await _partnersRepository.DeletePartnerByIdAsync(id);
    }

    //[HttpDelete]
    //public async Task DeletePartner(Partner partner)
    //{
    //    await _partnersDataManager.DeletePartnerAsync(partner);
    //}
}

