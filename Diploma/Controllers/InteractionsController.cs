using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

public class InteractionsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}