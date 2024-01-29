using Diploma.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectionsController(IDirectionsRepository directionsRepository): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDirections()
    {
        var directions = await directionsRepository.GetDirections();
        return new JsonResult(directions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDirection(int id)
    {
        var direction = await directionsRepository.GetDirection(id);
        return new JsonResult(direction);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDirection(int id)
    {
        await directionsRepository.DeleteDirection(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> AddDirection([FromBody] Direction direction)
    {
        await directionsRepository.AddDirection(direction);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDirection([FromBody] Direction direction)
    {
        await directionsRepository.UpdateDirection(direction);
        return Ok();
    }
}
