using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TruckOn.Trucks.DataAccess.Abstractions;
using TruckOn.Trucks.Models;
using TruckOn.Trucks.Models.QueryFilters;

namespace TruckOn.Trucks.DataAccess;

public class TruckEFRepository : ITruckRepository
{
    private readonly TruckEFContext db;

    public TruckEFRepository(TruckEFContext db)
    {
        this.db = db;
    }

    public async Task<bool> Create(Truck truck)
    {
        db.Trucks.Add(truck);
        int saveCount = await db.SaveChangesAsync();
        return saveCount == 1;
    }

    public async Task<bool> Delete(Truck truck)
    {
        db.Trucks.Remove(truck);
        int saveCount = await db.SaveChangesAsync();
        return saveCount == 1;
    }


    public async Task<Truck?> GetTruck(string code)
    {
        return await db.Trucks.SingleOrDefaultAsync(t => t.Code == code);
    }

    public async Task<PageResult<Truck>> GetTrucks(IEnumerable<IQueryFilter<Truck>> filters)
    {
        IQueryable<Truck> data = db.Trucks;

        List<IQueryFilter<Truck>> pagingfilters = new(2);

        foreach (var filter in filters)
        {
            if (filter is TakeFilter<Truck> || filter is SkipFilter<Truck>)
            {
                pagingfilters.Add(filter);
            }
            else
            {
                data = filter.Modify(data);
            }
        }

        int count = await data.CountAsync();

        foreach (var filter in pagingfilters)
        {
            data = filter.Modify(data);
        }

        return new PageResult<Truck>(count, await data.ToListAsync());
    }

    public async Task<bool> Update(Truck oldEntry, Truck newEntry)
    {
        db.Trucks.Entry(oldEntry).CurrentValues.SetValues(newEntry);

        if (db.ChangeTracker.HasChanges())
        {
            int saveCount = await db.SaveChangesAsync();
            return saveCount == 1;
        }
        else
        {
            return true;

        }
    }
}