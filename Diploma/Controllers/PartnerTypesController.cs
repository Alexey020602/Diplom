﻿using Microsoft.AspNetCore.Mvc;
using DataBase.Models;
using Diploma.Services;
namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartnerTypesController(IPartnerTypesRepository partnerTypesRepository) : ControllerBase
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
 
    [HttpPut]
    public async Task<IActionResult> AddPartnerType(PartnerType partnerType)
    {
        await partnerTypesRepository.AddPartnerTypeAsync(partnerType);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task DeletePartnerType(int id)
    {
        await partnerTypesRepository.DeletePartnerTypeByIdAsync(id);
    }

    [HttpDelete] 
    public async Task DeletePartnerType(PartnerType partnerType)
    {
        await partnerTypesRepository.DeletePartnerTypeAsync(partnerType);
    }

    [HttpPost] 
    public async Task UpdatePartnerType(PartnerType partnerType)
    {
        await partnerTypesRepository.UpdatePartnerTypeAsync(partnerType);
    }
}
