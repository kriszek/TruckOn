using TruckOn.Trucks.Controllers.Contracts;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Controllers.Mapping;

public static class TruckFieldsMap
{
    /// <summary>
    /// Names of corresponding properties between DTO and POCO
    /// </summary>
    public static readonly (string, string)[] FieldMap = new (string, string)[] {
            (nameof(TruckDTO.TruckStatus), nameof(Truck.Status)),
            (nameof(TruckDTO.TruckDescription), nameof(Truck.Description)),
            (nameof(TruckDTO.TruckCode), nameof(Truck.Code)),
            (nameof(TruckDTO.TruckName), nameof(Truck.Name))
        };

    /// <summary>
    /// Names of corresponding sort directives between DTO and POCO
    /// </summary>
    public static readonly (string, string)[] SortOrderMap = ConstructSortOrderMap();

    private static (string, string)[] ConstructSortOrderMap()
    {
        (string, string)[] orderMap = new (string, string)[FieldMap.Length * 2];

        for (int i = 0, j = 0; i < FieldMap.Length; i++, j = i * 2)
        {
            var fields = FieldMap[i];
            orderMap[j] = fields;
            orderMap[j + 1] = ($"{fields.Item1} desc", $"{fields.Item2} desc");
        }

        return orderMap;
    }
}
