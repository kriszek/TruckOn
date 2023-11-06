using Microsoft.Extensions.DependencyInjection;

namespace TruckOn.Trucks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTruckServices(this IServiceCollection services)
    {
        // services.AddScoped<,>();

        return services;
    }
}