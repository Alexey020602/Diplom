using Diploma.Services;
using Microsoft.AspNetCore.Mvc;
using Model.Divisions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Diploma.Controllers;

public class FacultiesController(IFacultyRepository repository) : ApiControllerBase
{
    // GET: api/<FacultiesController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return new JsonResult(await repository.GetFaculties());
    }


    // GET api/<FacultiesController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return new JsonResult(await repository.GetFaculty(id));
    }

    // POST api/<FacultiesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Faculty faculty)
    {
        await repository.AddFaculty(faculty);
        return Created();
    }


    // PUT api/<FacultiesController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Faculty faculty)
    {
        await repository.UpdateFaculty(id, faculty);
        return NoContent();
    }

    // DELETE api/<FacultiesController>/5
    [HttpDelete("{id}")]
    public Task Delete(int id)
    {
        return repository.DeleteFaculty(id);
    }
}