using Diploma.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Interactions;

namespace Diploma.Controllers;

public class InteractionsController(IInteractionRepository interactionRepository) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(int? interactionTypeId = null)
    {
        return new JsonResult(
            (await interactionRepository.GetInteractions(interactionTypeId))
            .Select(i => new InteractionShort(i.Id, i.ToString()))
        );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetInteraction(int id)
    {
        return new JsonResult(
            await interactionRepository.GetInteractionById(id)
        );
    }

    [HttpPost]
    public async Task<IActionResult> AddInteraction(Interaction interaction)
    {
        await interactionRepository.AddInteraction(interaction);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateInteraction(int id, Interaction interaction)
    {
        await interactionRepository.UpdateInteraction(id, interaction);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await interactionRepository.DeleteInteractionById(id);
        return Ok();
    }
}