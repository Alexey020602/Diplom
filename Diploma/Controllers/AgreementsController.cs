using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

public class AgreementsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}