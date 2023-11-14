namespace TruckOn.Trucks.Models;

public record PageResult<T>(int TotalCount, IEnumerable<T> Data);
