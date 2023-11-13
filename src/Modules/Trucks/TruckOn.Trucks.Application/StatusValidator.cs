using System.Collections.Concurrent;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Application;

public class StatusValidator : IStatusValidator
{
    private static readonly IReadOnlyDictionary<TruckStatus, TruckStatus> allowedChanges = new ConcurrentDictionary<TruckStatus, TruckStatus>
    {
        [TruckStatus.ToJob] = TruckStatus.Loading,
        [TruckStatus.AtJob] = TruckStatus.ToJob,
        [TruckStatus.Returning] = TruckStatus.AtJob,
        [TruckStatus.Loading] = TruckStatus.Returning,
    };

    public bool IsNewStatusProper(TruckStatus newStatus, TruckStatus oldStatus)
    {
        if (newStatus == oldStatus) return true;
        if (newStatus == TruckStatus.OutOfService) return true;
        if (oldStatus == TruckStatus.OutOfService) return true;

        return allowedChanges[newStatus] == oldStatus;
    }

}
