using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TruckOn.Shared;
using TruckOn.Trucks.Application;
using TruckOn.Trucks.Application.Abstractions;
using TruckOn.Trucks.Controllers.Contracts;
using TruckOn.Trucks.Controllers.Validation;
using TruckOn.Trucks.DataAccess;
using TruckOn.Trucks.DataAccess.Abstractions;

namespace TruckOn.Trucks.Infrastructure;

/// <summary>
/// Extension methods configuring services for Trucks module
/// </summary>
public class TrucksServiceRegistrator : IServiceRegistrator
{
    public IServiceCollection AddModuleServices(IServiceCollection services)
    {
        return services
            .AddScoped<IValidator<TruckDTO>, TruckDTOValidator>()
            .AddScoped<ITrucksService, TrucksService>()
            // .AddScoped<ITruckRepository, TruckDictionaryRepository>();
            .AddScoped<ITruckRepository, TruckEFRepository>()
            .AddDbContext<TruckEFContext>(options =>
                options.UseSqlite("DataSource=Tracks.db"));
        ;
    }
}