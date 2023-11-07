using Microsoft.Extensions.DependencyInjection;
using TruckOn.Trucks.Application;
using TruckOn.Trucks.Application.Abstractions;
using TruckOn.Trucks.DataAccess;
using TruckOn.Trucks.DataAccess.Abstractions;

namespace TruckOn.Trucks.Infrastructure;

/// <summary>
/// Extension methods configuring services for Trucks module
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddTruckServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ITrucksService, TrucksService>()
            .AddScoped<ITruckRepository, TruckRepository>();
    }
}