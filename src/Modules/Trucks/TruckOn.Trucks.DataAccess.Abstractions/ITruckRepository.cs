using TruckOn.Trucks.Models;
using TruckOn.Trucks.Models.QueryFilters;

namespace TruckOn.Trucks.DataAccess.Abstractions
{
    /// <summary>
    /// Truck repository interface
    /// </summary>
    public interface ITruckRepository
    {
        Task<Truck?> GetTruck(string code);
        Task<bool> Create(Truck truck);
        Task<bool> Update(Truck oldEntry, Truck newEntry);
        Task<PageResult<Truck>> GetTrucks(IEnumerable<IQueryFilter<Truck>> filters);
    }
}
