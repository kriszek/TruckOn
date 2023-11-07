using Microsoft.AspNetCore.Mvc;
using TruckOn.Trucks.Application.Abstractions;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Controllers;

/// <summary>
/// Truck controller
/// </summary>
[ApiController]
[Route("truck")]
public class TruckController : ControllerBase
{
    private readonly ITrucksService trucksService;

    public TruckController(ITrucksService trucksService)
    {
        this.trucksService = trucksService;
    }

    [HttpGet("get/{code}")]
    public IActionResult Get(string code)
    {
        Truck truck = trucksService.GetTruck(code);

        return Ok(truck);
    }

    // very simple healthcheck method
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok("good");
    }
}