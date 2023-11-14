using System.Linq;
using TruckOn.Trucks.DataAccess.Abstractions;
using TruckOn.Trucks.Models;
using TruckOn.Trucks.Models.QueryFilters;

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

        public Task<bool> Delete(Truck truck)
        {
            trucks.Remove(truck.Code);
            return Task.FromResult(true);
        }

        public Task<Truck?> GetTruck(string code)
        {
            return Task.FromResult(trucks.GetValueOrDefault(code));
        }

        public Task<PageResult<Truck>> GetTrucks(IEnumerable<IQueryFilter<Truck>> filters)
        {
            IQueryable<Truck> data = trucks.Values.AsQueryable();

            List<IQueryFilter<Truck>> pagingfilters = new(filters.Count());

            foreach (var filter in filters)
            {
                if (filter is TakeFilter<Truck> || filter is SkipFilter<Truck> || filter is OrderByFilter<Truck>)
                {
                    pagingfilters.Add(filter);
                }
                else
                {
                    data = filter.Modify(data);
                }
            }

            int count = data.Count();

            foreach (var filter in pagingfilters)
            {
                data = filter.Modify(data);
            }

            return Task.FromResult(new PageResult<Truck>(count, data.ToList()));
        }

        public Task<bool> Update(Truck oldEntry, Truck newEntry)
        {
            trucks[oldEntry.Code] = newEntry;

            return Task.FromResult(true);
        }
    }
}
