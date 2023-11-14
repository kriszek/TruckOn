using Mapster;
using TruckOn.Trucks.Controllers.Contracts;
using TruckOn.Trucks.Models;
using TruckOn.Trucks.Models.QueryFilters;

namespace TruckOn.Trucks.Controllers.Mapping;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<TruckDTO, Truck>()
            .Map(dest => dest.Code, src => src.TruckCode)
            .Map(dest => dest.Name, src => src.TruckName)
            .Map(dest => dest.Status, src => src.TruckStatus)
            .Map(dest => dest.Description, src => src.TruckDescription);

        config
            .NewConfig<Truck, TruckDTO>()
            .Map(dest => dest.TruckCode, src => src.Code)
            .Map(dest => dest.TruckName, src => src.Name)
            .Map(dest => dest.TruckStatus, src => src.Status)
            .Map(dest => dest.TruckDescription, src => src.Description);

        config
            .NewConfig<TruckQueryDTO, IEnumerable<IQueryFilter<Truck>>>()
            .MapWith(x => TruckQueryDTOMap(x));

        config
            .NewConfig<PageResult<Truck>, PagedResponse<TruckDTO>>()
            .Map(dest => dest.TotalCount, src => src.TotalCount)
            .Map(dest => dest.PageData, src => src.Data);
    }

    private IEnumerable<IQueryFilter<Truck>> TruckQueryDTOMap(TruckQueryDTO queryDto)
    {
        List<IQueryFilter<Truck>> filters = new();

        if (!string.IsNullOrEmpty(queryDto.TruckName))
        {
            filters.Add(new TruckNameContainsText(queryDto.TruckName));
        }

        if (!string.IsNullOrEmpty(queryDto.TruckDescription))
        {
            filters.Add(new TruckDescriptionContainsText(queryDto.TruckDescription));
        }

        if (queryDto.TruckStatus is not null && queryDto.TruckStatus.Length > 0)
        {
            filters.Add(new TruckStatusOnList(queryDto.TruckStatus));
        }

        if (queryDto.PageNumber is not null && queryDto.PageSize is not null)
        {
            filters.Add(new SkipFilter<Truck>((queryDto.PageNumber.Value - 1) * queryDto.PageSize.Value));
            filters.Add(new TakeFilter<Truck>(queryDto.PageSize.Value));
        }

        if (queryDto.Order is not null)
        {
            // because model and dto have different property names
            var modelOrdering = queryDto.Order.Select(o => TruckFieldsMap.SortOrderMap.Single(m => string.Equals(m.Item1, o, StringComparison.InvariantCultureIgnoreCase)).Item2).ToArray();
            filters.Add(new OrderByFilter<Truck>(modelOrdering));
        }

        return filters;
    }
}
