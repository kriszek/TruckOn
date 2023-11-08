using Mapster;
using TruckOn.Trucks.Controllers.Contracts;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Controllers.Mapping;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<TruckDTO, Truck>()
            .Map(dest => dest.Code, src => src.TruckCode)
            .Map(dest => dest.Name, src => src.TruckName);

        config
            .NewConfig<Truck, TruckDTO>()
            .Map(dest => dest.TruckCode, src => src.Code)
            .Map(dest => dest.TruckName, src => src.Name);
    }
}
