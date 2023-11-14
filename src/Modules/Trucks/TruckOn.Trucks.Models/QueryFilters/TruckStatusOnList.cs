namespace TruckOn.Trucks.Models.QueryFilters;

public class TruckStatusOnList : IQueryFilter<Truck>
{
    private readonly IEnumerable<TruckStatus> statusList;

    public TruckStatusOnList(IEnumerable<TruckStatus> statusList)
    {
        this.statusList = statusList;
    }

    public IQueryable<Truck> Modify(IQueryable<Truck> query)
    {
        return query.Where(t => statusList.Contains(t.Status));
    }
}
