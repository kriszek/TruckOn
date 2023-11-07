using ErrorOr;
using Microsoft.AspNetCore.Http;
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

    [HttpGet("{code}")]
    public async Task<IActionResult> Get(string code)
    {
        Truck? truck = await trucksService.GetTruck(code);

        return truck is null ? NotFound() : Ok(truck);
    }

    [HttpPost()]
    public async Task<IActionResult> Create(Truck truck)
    {
        var result = await trucksService.CreateTruck(truck);

        return result.MatchFirst(
            isSaved => isSaved ? Ok() : InternalProblem("Save failed"),
            error => Problemm(error));
    }

    private IActionResult InternalProblem(string description)
    {
        return Problem(statusCode: StatusCodes.Status500InternalServerError, title: description);
    }

    private IActionResult Problemm(Error error)
    {
        var statusCode = error.Type switch  
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }



    /// <summary>
    /// Very simple healthcheck method
    /// </summary>
    /// <returns>HTTP 200 if service is alive</returns>
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok("good");
    }
}