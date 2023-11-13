using ErrorOr;

namespace TruckOn.Trucks.Models
{
    /// <summary>
    /// Errors returned from application
    /// </summary>
    public class Errors
    {
        public static readonly Error NotFound = Error.NotFound("Truck.NotFound", "Truck not found.");
        public static readonly Error DuplicateCode = Error.Conflict("Truck.DuplicateCode", "Truck with given code already exists.");
        public static readonly Error SaveFailed = Error.Failure("Truck.SaveFailed", "Persisting data failed.");
        public static readonly Error InvalidStatus = Error.Validation("Truck.InvalidStatus", "Improper new status.");
    }
}
