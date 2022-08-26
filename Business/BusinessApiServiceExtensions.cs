using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FluentAssertions;

public static class BusinessApiServiceExtensions
{
    public static void AddServicesForAssembly(this IServiceCollection services, IConfiguration configuration)
    {
        Type interfaceType = typeof(IService);
        IEnumerable<TypeInfo> serviceTypesInAssembly = interfaceType.Assembly.DefinedTypes
            .Where(x => !x.IsAbstract && !x.IsInterface && interfaceType.IsAssignableFrom(x));
        foreach (var serviceType in serviceTypesInAssembly)
        {
            // add the service itself
            services.TryAdd(new ServiceDescriptor(serviceType, serviceType, ServiceLifetime.Singleton)); // runtime error, I think

            // add any required service dependencies, besides the service (e.g. what's needed for construction)
            serviceType.GetMethod(nameof(IStaticService.AddServiceDependencies))! // BOOTSTRAPPING EXAMPLE
                .Invoke(null, new object[] {services, configuration});
        }
    } 
}