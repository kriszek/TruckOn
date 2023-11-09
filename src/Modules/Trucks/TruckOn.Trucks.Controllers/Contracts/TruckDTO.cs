namespace TruckOn.Trucks.Controllers.Contracts;

public class TruckDTO
{
        public string TruckCode { get; set; } = default!;

        public string TruckName { get; set; } = default!;

        public string TruckStatus { get; set; } = default!;

        public string? TruckDescription { get; set; } = default!;
}