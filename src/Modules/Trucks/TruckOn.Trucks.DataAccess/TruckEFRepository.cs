using Microsoft.EntityFrameworkCore;
using TruckOn.Trucks.DataAccess.Abstractions;
using TruckOn.Trucks.Models;

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

    public async Task<Truck?> GetTruck(string code)
    {
        return await db.Trucks.SingleOrDefaultAsync(t => t.Code == code);
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