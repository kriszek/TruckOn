namespace TruckOn.Trucks.Models.QueryFilters;

public class TakeFilter<T> : IQueryFilter<T>
{
    private readonly int count;

    public TakeFilter(int count)
    {
        this.count = count;
    }

    public IQueryable<T> Modify(IQueryable<T> query)
    {
        return query.Take(count);
    }
}
