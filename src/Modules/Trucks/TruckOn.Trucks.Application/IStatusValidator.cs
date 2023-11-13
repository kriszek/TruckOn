using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Application
{
    public interface IStatusValidator
    {
        bool IsNewStatusProper(TruckStatus newStatus, TruckStatus OldStatus);
    }

}
