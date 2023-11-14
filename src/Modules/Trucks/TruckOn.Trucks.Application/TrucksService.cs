using ErrorOr;
using TruckOn.Trucks.Application.Abstractions;
using TruckOn.Trucks.DataAccess.Abstractions;
using TruckOn.Trucks.Models;
using TruckOn.Trucks.Models.QueryFilters;

namespace TruckOn.Trucks.Application
{
    /// <summary>
    /// Trucks Service
    /// </summary>
    public class TrucksService : ITrucksService
    {
        private readonly ITruckRepository truckRepository;
        private readonly IStatusValidator statusValidator;


        public TrucksService(ITruckRepository truckRepository, IStatusValidator statusValidator)
        {
            this.truckRepository = truckRepository;
            this.statusValidator = statusValidator;
        }

        public async Task<ErrorOr<bool>> UpsertTruck(Truck truck)
        {
            var persistedTruck = await truckRepository.GetTruck(truck.Code);

            if (persistedTruck is null)
            {
                return await truckRepository.Create(truck) ? true : Errors.SaveFailed;
            }

            if (statusValidator.IsNewStatusProper(truck.Status, persistedTruck.Status))
            {
                return await truckRepository.Update(persistedTruck, truck) ? false : Errors.SaveFailed;
            }
            else
            {
                return Errors.InvalidStatus;
            }
        }

        public async Task<Truck?> GetTruck(string code)
        {
            return await truckRepository.GetTruck(code);
        }

        public async Task<PageResult<Truck>> GetTrucks(IEnumerable<IQueryFilter<Truck>> filters)
        {
            return await truckRepository.GetTrucks(filters);
        }
    }
}
