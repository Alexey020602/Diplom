﻿using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Interactions;

namespace Diploma.Controllers;

public class InteractionsController(IInteractionRepository interactionRepository) : ApiControllerBase
{
    [Authorize(Roles = "Cip")]
    [HttpGet("count")]
    public Task<int> GetCount() => interactionRepository.InteractionsCountAsync();
    
    [Authorize(Roles = "Cip")]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] InteractionsFilter filter)
    {
        return new JsonResult(
            (await interactionRepository.GetInteractions(filter)));
    }

    [Authorize(Roles = "Cip")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetInteraction(int id)
    {
        return new JsonResult(
            await interactionRepository.GetInteractionById(id)
        );
    }

    [HttpPost]
    [Authorize(Roles = "Ctt")]
    public async Task<IActionResult> AddInteraction(Interaction interaction)
    {
        await interactionRepository.AddInteraction(interaction);
        return Ok();
    }

    [Authorize(Roles = "Ctt")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateInteraction(int id, Interaction interaction)
    {
        await interactionRepository.UpdateInteraction(id, interaction);
        return Ok();
    }

    [Authorize(Roles = "Ctt")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await interactionRepository.DeleteInteractionById(id);
        return Ok();
    }
}