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

        public async Task<ErrorOr<bool>> CreateTruck(Truck truck)
        {
            if(await truckRepository.GetTruck(truck.Code) is not null)
            {
                return Errors.DuplicateCode;
            }

            return await truckRepository.Create(truck);
        }


        public async Task<Truck?> GetTruck(string code)
        {
            return await truckRepository.GetTruck(code);
        }
    }
}
