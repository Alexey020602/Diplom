using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Interactions;

namespace Diploma.Controllers;

public class InteractionsController(IInteractionRepository interactionRepository) : ApiControllerBase
{
    [Authorize(Roles = "Cip")]
    [HttpGet]
    public async Task<IActionResult> Get(
        string? code = null, 
        int? interactionTypeId = null, 
        DateOnly? sign = null, 
        DateOnly? start = null,
        DateOnly? end = null, 
        int skip = 0,
        int take = 10)
    {
        return new JsonResult(
            (await interactionRepository.GetInteractions(
                code, 
                interactionTypeId, 
                sign, 
                start,
                end,
                skip,
                take))
            .Select(i => new InteractionShort(i.Id, i.ToString()))
        );
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