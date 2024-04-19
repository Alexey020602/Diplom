using DataBase.Models;
using Diploma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModel;

namespace Diploma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractonsController(IInteractionRepository interactionRepository): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(int? interactionTypeId = null) => new JsonResult(
            (await interactionRepository.GetInteractions(interactionTypeId))
            .Select(i => new InteractionShort(i.Id, i.ToString()))
            );
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInteraction(int id) => new JsonResult(
            await interactionRepository.GetInteractionById(id)
            );

        [HttpPost] public async Task<IActionResult> AddInteraction(Interaction interaction)
        {
            await interactionRepository.AddInteraction(interaction);
            return Ok();
        }

        [HttpPut("{id:int}")] public async Task<IActionResult> UpdateInteraction(int id, Interaction interaction)
        {
            await interactionRepository.UpdateInteraction(id, interaction);
            return Ok();
        }

        [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id)
        {
            await interactionRepository.DeleteInteractionById(id);
            return Ok();
        }
    }
}
