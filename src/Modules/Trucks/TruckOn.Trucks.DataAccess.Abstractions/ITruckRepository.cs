using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.DataAccess.Abstractions
{
    /// <summary>
    /// Truck repository interface
    /// </summary>
    public interface ITruckRepository
    {
        Truck GetTruck(string code);
    }
}
