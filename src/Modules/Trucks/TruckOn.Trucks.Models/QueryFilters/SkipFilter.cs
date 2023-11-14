namespace TruckOn.Trucks.Models.QueryFilters;

public class SkipFilter<T> : IQueryFilter<T>
{
    private readonly int count;

    public SkipFilter(int count)
    {
        this.count = count;
    }

    public IQueryable<T> Modify(IQueryable<T> query)
    {
        return query.Skip(count);
    }
}
