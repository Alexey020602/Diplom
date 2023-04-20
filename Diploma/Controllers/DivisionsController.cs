using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
public class DivisionsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}