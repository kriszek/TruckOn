using ErrorOr;
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

        public async Task<ErrorOr<bool>> UpsertTruck(Truck truck)
        {
            var persistedTruck = await truckRepository.GetTruck(truck.Code);

            if (persistedTruck is not null)
            {
                return await truckRepository.Update(persistedTruck, truck) ? false : Errors.SaveFailed;
            }
            else
            {
                return await truckRepository.Create(truck) ? true : Errors.SaveFailed;
            }
        }

        public async Task<Truck?> GetTruck(string code)
        {
            return await truckRepository.GetTruck(code);
        }
    }
}
