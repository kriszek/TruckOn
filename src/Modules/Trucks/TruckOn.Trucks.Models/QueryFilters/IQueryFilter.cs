namespace TruckOn.Trucks.Models.QueryFilters;

public interface IQueryFilter<T>
{
    IQueryable<T> Modify(IQueryable<T> query);
}