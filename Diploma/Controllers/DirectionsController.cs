using Diploma.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Diploma.Controllers;

public class DirectionsController(IDirectionsRepository directionsRepository) : ApiControllerBase
{
    [Authorize(Roles = "Cip")]
    [HttpGet]
    public async Task<IActionResult> GetDirections()
    {
        var directions = await directionsRepository.GetDirections();
        return new JsonResult(directions);
    }

    [Authorize(Roles = "Cip")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDirection(int id)
    {
        var direction = await directionsRepository.GetDirection(id);
        return new JsonResult(direction);
    }

    [Authorize(Roles = "Ctt")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDirection(int id)
    {
        await directionsRepository.DeleteDirection(id);
        return Ok();
    }

    [Authorize(Roles = "Ctt")]
    [HttpPost]
    public async Task<IActionResult> AddDirection([FromBody] Direction direction)
    {
        await directionsRepository.AddDirection(direction);
        return Ok();
    }

    [Authorize(Roles = "Ctt")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDirection(int id, [FromBody] Direction direction)
    {
        await directionsRepository.UpdateDirection(id, direction);
        return Ok();
    }
}