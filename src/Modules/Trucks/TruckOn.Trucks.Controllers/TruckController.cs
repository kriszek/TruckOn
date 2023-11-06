using Microsoft.AspNetCore.Mvc;

namespace TruckOn.Trucks.Controllers;

[ApiController]
[Route("truck")]
public class TruckController : ControllerBase
{
    // very simple healthcheck method
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok("good");
    }
}