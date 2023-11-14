namespace TruckOn.Trucks.Controllers.Contracts;

public record PagedResponse<T>
{
    public int TotalCount { get; set; }
    public IEnumerable<T> PageData { get; set; } = default!;
}
