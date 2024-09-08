using Diploma.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Partners;

namespace Diploma.Controllers;

public class PartnerTypesController(IPartnerTypesRepository partnerTypesRepository) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPartnerTypes()
    {
        var partnerTypes = await partnerTypesRepository.GetAllPartnerTypesAsync();
        return new JsonResult(partnerTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPartnerType(int id)
    {
        var partnerType = await partnerTypesRepository.GetPartnerTypeByIdAsync(id);
        return new JsonResult(partnerType);
    }

    [HttpPost]
    public async Task<IActionResult> AddPartnerType(PartnerType partnerPartnerType)
    {
        await partnerTypesRepository.AddPartnerTypeAsync(partnerPartnerType);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task DeletePartnerType(int id)
    {
        await partnerTypesRepository.DeletePartnerTypeByIdAsync(id);
    }

    [HttpPut("{id}")]
    public async Task UpdatePartnerType(int id, PartnerType partnerPartnerType)
    {
        await partnerTypesRepository.UpdatePartnerTypeAsync(id, partnerPartnerType);
    }
}