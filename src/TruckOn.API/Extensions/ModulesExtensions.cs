using TruckOn.Trucks.Controllers;
using TruckOn.Trucks.Infrastructure;

namespace TruckOn.API.Extensions
{
    /// <summary>
    /// Placo for modules code
    /// </summary>
    public static class ModulesExtensions
    {
        public static IMvcBuilder AddModules(this IMvcBuilder builder)
        {
            return builder
                    .AddApplicationParts()
                    .AddApplicationServices();
        }
        
        private static IMvcBuilder AddApplicationParts(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddApplicationPart(typeof(TruckController).Assembly);
        }
        
        private static IMvcBuilder AddApplicationServices(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.Services.AddTruckServices();

            return mvcBuilder;
        }
    }
}
