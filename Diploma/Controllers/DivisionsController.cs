using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

public class DivisionsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}