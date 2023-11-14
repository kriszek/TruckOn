namespace TruckOn.Trucks.Models.QueryFilters;

public class TruckNameContainsText : IQueryFilter<Truck>
{
    public TruckNameContainsText(string text)
    {
        Text = text;
    }

    public string Text { get; }

    public IQueryable<Truck> Modify(IQueryable<Truck> query)
    {
        return query.Where(t => t.Name.Contains(Text));
    }
}
