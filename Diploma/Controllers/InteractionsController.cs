﻿using DataBase.Models;
using Diploma.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Extensions;
using Model.Interactions;
using Interaction = DataBase.Models.Interaction;

namespace Diploma.Controllers
{
    public class InteractionsController(IInteractionRepository interactionRepository): ApiControllerBase
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

        [HttpPost] public async Task<IActionResult> AddInteraction(Model.Interactions.Interaction interaction)
        {
            await interactionRepository.AddInteraction(interaction);
            return Ok();
        }

        [HttpPut("{id:int}")] public async Task<IActionResult> UpdateInteraction(int id, Model.Interactions.Interaction interaction)
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
