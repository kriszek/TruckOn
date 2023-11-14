using System.Linq.Dynamic.Core;

namespace TruckOn.Trucks.Models.QueryFilters;

public class OrderByFilter<T> : IQueryFilter<T>
{
    public OrderByFilter(string[] columns)
    {
        Columns = columns;
    }

    public string[] Columns { get; }

    public IQueryable<T> Modify(IQueryable<T> query)
    {
        IOrderedQueryable<T> q = query.OrderBy(Columns[0]);

        for (int i = 1; i < Columns.Length; i++)
        {
            q = q.ThenBy(Columns[i]);
        }

        return q;
    }
}
