using ErrorOr;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Application.Abstractions
{
    /// <summary>
    /// Trucks Service interface
    /// </summary>
    public interface ITrucksService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="truck"></param>
        /// <returns>true if new truck was created, false when it was updated, error when action failed</returns>
        Task<ErrorOr<bool>> UpsertTruck(Truck truck);

        Task<Truck?> GetTruck(string code);
    }
}
