namespace TruckOn.Trucks.Models
{
    public enum TruckStatus : byte
    {
        OutOfService = 1,
        Loading,
        ToJob,
        AtJob,
        Returning
    }
}
