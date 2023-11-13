using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TruckOn.Shared;
using TruckOn.Trucks.Application;
using TruckOn.Trucks.Application.Abstractions;
using TruckOn.Trucks.Controllers.Contracts;
using TruckOn.Trucks.Controllers.Validation;
using TruckOn.Trucks.DataAccess;
using TruckOn.Trucks.DataAccess.Abstractions;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Infrastructure;

/// <summary>
/// Extension methods configuring services for Trucks module
/// </summary>
public class TrucksServiceRegistrator : IServiceRegistrator
{
    public IServiceCollection AddModuleServices(IServiceCollection services, IConfiguration configuration)
    {
        TrucksOptions trucksOptions = configuration.GetSection("Trucks").Get<TrucksOptions>();

        return services
            .AddSingleton(trucksOptions)
            .AddScoped<IValidator<TruckDTO>, TruckDTOValidator>()
            .AddScoped<ITrucksService, TrucksService>()
            // .AddScoped<ITruckRepository, TruckDictionaryRepository>();
            .AddScoped<ITruckRepository, TruckEFRepository>()
            .AddDbContext<TruckEFContext>(options =>
                options.UseSqlite(trucksOptions.EFConnectionString));
        ;
    }
}