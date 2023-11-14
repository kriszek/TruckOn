using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Controllers.Contracts;

public class TruckQueryDTO : PagedQuery
{
    public string? TruckName { get; set; }

    public TruckStatus[]? TruckStatus { get; set; }

    public string? TruckDescription { get; set; }
}
