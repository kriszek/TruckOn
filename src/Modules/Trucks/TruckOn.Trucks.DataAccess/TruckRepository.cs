using TruckOn.Trucks.DataAccess.Abstractions;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.DataAccess
{
    /// <summary>
    /// Truck repository
    /// </summary>
    public class TruckRepository : ITruckRepository
    {
        public Truck GetTruck(string code)
        {
            return new Truck(code, "Yellow Truck");
        }
    }
}
