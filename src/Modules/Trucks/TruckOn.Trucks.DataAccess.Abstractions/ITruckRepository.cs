using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.DataAccess.Abstractions
{
    /// <summary>
    /// Truck repository interface
    /// </summary>
    public interface ITruckRepository
    {
        Task<Truck?> GetTruck(string code);
        Task<bool> Create(Truck truck);
    }
}
