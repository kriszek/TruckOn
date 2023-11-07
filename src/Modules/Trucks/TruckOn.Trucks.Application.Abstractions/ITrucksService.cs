using ErrorOr;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Application.Abstractions
{
    /// <summary>
    /// Trucks Service interface
    /// </summary>
    public interface ITrucksService
    {
        Task<ErrorOr<bool>> CreateTruck(Truck truck);

        Task<Truck?> GetTruck(string code);
    }
}
