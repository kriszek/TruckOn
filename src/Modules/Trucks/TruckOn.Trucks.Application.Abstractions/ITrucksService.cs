using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Application.Abstractions
{
    /// <summary>
    /// Trucks Service interface
    /// </summary>
    public interface ITrucksService
    {
        Truck GetTruck(string code);
    }
}
