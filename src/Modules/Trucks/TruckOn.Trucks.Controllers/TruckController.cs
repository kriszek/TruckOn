using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruckOn.Trucks.Application.Abstractions;
using TruckOn.Trucks.Controllers.Contracts;
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
    private readonly IMapper mapper;

    public TruckController(ITrucksService trucksService, IMapper mapper)
    {
        this.trucksService = trucksService;
        this.mapper = mapper;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> Get(string code)
    {
        Truck? truck = await trucksService.GetTruck(code);

        return truck is null ? NotFound() : Ok(mapper.Map<TruckDTO>(truck));
    }

    [HttpPut()]
    public async Task<IActionResult> Upsert(TruckDTO truck)
    {
        var result = await trucksService.UpsertTruck(mapper.Map<Truck>(truck));

        return result.MatchFirst(
            isNewCretaed => isNewCretaed ? CreatedAtAction(nameof(Get), new { code = truck.TruckCode }, null) : NoContent(),
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