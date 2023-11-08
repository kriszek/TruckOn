using Microsoft.Extensions.DependencyInjection;

namespace TruckOn.Shared;

/// <summary>
/// Is automatically search for in modules and called for service registration
/// </summary>
public interface IServiceRegistrator
{
    IServiceCollection AddModuleServices(IServiceCollection services);
}
