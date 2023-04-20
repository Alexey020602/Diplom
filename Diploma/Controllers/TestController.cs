using Microsoft.AspNetCore.Mvc;
using DataBase.Data;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private ApplicationContext _context { get; set; }

    public TestController(ApplicationContext context)
    {
        this._context = context;
    }

    [HttpGet(Name = "GetTest")]
    public IActionResult Get()
    {
        var list = 
            from p in _context.Partners 
            //where p.
            select p;

        return new JsonResult(list);
    }
}