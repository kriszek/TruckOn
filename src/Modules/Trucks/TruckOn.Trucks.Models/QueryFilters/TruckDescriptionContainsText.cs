namespace TruckOn.Trucks.Models.QueryFilters;

public class TruckDescriptionContainsText : IQueryFilter<Truck>
{
    public TruckDescriptionContainsText(string text)
    {
        Text = text;
    }

    public string Text { get; }

    public IQueryable<Truck> Modify(IQueryable<Truck> query)
    {
        return query.Where(t => t.Description != null && t.Description.Contains(Text));
    }
}
