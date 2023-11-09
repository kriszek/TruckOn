using Microsoft.EntityFrameworkCore;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.DataAccess;

public class TruckEFContext : DbContext
{
    public TruckEFContext(DbContextOptions<TruckEFContext> options) : base(options)
    {
    }

    public DbSet<Truck> Trucks { get; set; }
}
