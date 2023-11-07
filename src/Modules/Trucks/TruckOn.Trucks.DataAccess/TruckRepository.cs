using System.Security.Permissions;
using TruckOn.Trucks.DataAccess.Abstractions;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.DataAccess
{
    /// <summary>
    /// Truck repository
    /// </summary>
    public class TruckRepository : ITruckRepository
    {
        private static readonly Dictionary<string, Truck> trucks = new();

        public Task<bool> Create(Truck truck)
        {
            trucks.Add(truck.Code, truck);

            return Task.FromResult(true);
        }


        public Task<Truck?> GetTruck(string code)
        {
            return Task.FromResult(trucks.GetValueOrDefault(code));
        }
    }
}
