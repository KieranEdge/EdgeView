using Microsoft.AspNetCore.Mvc;

namespace EdgeView.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("EdgeView API is running 🚀");
}
