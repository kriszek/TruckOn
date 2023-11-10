using TruckOn.Trucks.DataAccess.Abstractions;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.DataAccess
{
    /// <summary>
    /// Truck repository
    /// </summary>
    public class TruckDictionaryRepository : ITruckRepository
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

        public Task<bool> Update(Truck oldEntry, Truck newEntry)
        {
            trucks[oldEntry.Code] = newEntry;

            return Task.FromResult(true);
        }
    }
}
