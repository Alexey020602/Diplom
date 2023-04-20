using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
public class AgreementsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}