using System.Reflection;
using Mapster;
using Microsoft.VisualBasic;
using TruckOn.Shared;
using TruckOn.Trucks.Controllers;
using TruckOn.Trucks.Infrastructure;

namespace TruckOn.API.Extensions
{
    /// <summary>
    /// Placo for modules code
    /// </summary>
    public static class ModulesExtensions
    {
        // add here one type from each module's controller project
        // to automatically use module
        private static readonly (Type controller, Type di )[] moduleTypes = { (typeof(TruckController), typeof(TrucksServiceRegistrator)) };

        public static IMvcBuilder AddModules(this IMvcBuilder mvcBuilder)
        {
            var builder = mvcBuilder;

            foreach ((Type controller, Type di ) in moduleTypes)
            {
                Assembly controllerAssembly = controller.Assembly;

                builder = builder.AddApplicationPart(controllerAssembly)
                                .AddApplicationServices(di.Assembly);

                // add mapster mappings                
                TypeAdapterConfig.GlobalSettings.Scan(controllerAssembly);
            }

            return builder;
        }

        private static IMvcBuilder AddApplicationServices(this IMvcBuilder mvcBuilder, Assembly assembly)
        {
            var instances = from t in assembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(IServiceRegistrator))
                                     && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as IServiceRegistrator;

            foreach (var instance in instances)
            {
                instance.AddModuleServices(mvcBuilder.Services);
            }

            return mvcBuilder;
        }
    }
}
