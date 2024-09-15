using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
}