using TruckOn.Trucks.Application.Abstractions;
using TruckOn.Trucks.DataAccess.Abstractions;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Application
{
    /// <summary>
    /// Trucks Service
    /// </summary>
    public class TrucksService : ITrucksService
    {
        private readonly ITruckRepository truckRepository;

        public TrucksService(ITruckRepository truckRepository)
        {
            this.truckRepository = truckRepository;
        }

        public Truck GetTruck(string code)
        {
            return truckRepository.GetTruck(code);
        }
    }
}
