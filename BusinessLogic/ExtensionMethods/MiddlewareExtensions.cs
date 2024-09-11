using LAF.BusinessLogic.ServiceResolver;
using LAF.Middleware;
using LAF.Models.Interfaces.Services;
using LAF.Services.Classes;
using LAF.Services.DataProviders.Interfaces;
using LAF.Services.Interfaces;
using LAF.Services.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LAF
{
    namespace BusinessLogic.ExtensionMethods
    {
        public static class RequestAgentMatchMiddlewareExtensions
        {
            public static IApplicationBuilder UseAgentMatchService(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<RequestAgentMatchMiddleware>();
            }
        }

        public static class ConfigServiceCollectionExtensions
        {
            public static IServiceCollection AddDataProviderServiceConfig(
                 this IServiceCollection services, IConfiguration config)
            {
                Dictionary<string, DataProviderOptions> dataProviderOptionsDict = config.GetDataProviderServiceOptions();
                
                //Add DataProviderOptions to ServiceProvider for each registered dataProvider type.
                foreach (var key in dataProviderOptionsDict.Keys)
                {
                    Type? tServiceName = Type.GetType($"LAF.Services.DataProviders.{dataProviderOptionsDict[key].ServiceType}Options, LAF.Services");

                    if (tServiceName != null)
                    {
                        Type? tInterface = tServiceName.GetInterface($"LAF.Services.DataProviders.I{dataProviderOptionsDict[key].ServiceType}Options");

                        if (tInterface != null)
                        {
                            DataProviderOptions options = dataProviderOptionsDict[key];

                            var provider = Activator.CreateInstance(tServiceName, options);

                            if (provider != null)
                            {
                                //Since we cannot specify generics at runtime, we cannot use the IOptions pattern.
                                //It looks like this is the only way of registering the data options subclasses as services.
                                //But these aren't managed services - they need to be disposed.
                                services.AddScoped(tInterface, implementationFactory: sProvider => { return provider; });
                            }
                        }
                        else
                        {
                            throw new Exception($"Interface LAF.Services.DataProviders.I{dataProviderOptionsDict[key].ServiceType}Options not found.");
                        }
                    }
                    else
                    {
                        throw new Exception($"Class LAF.Services.DataProviders.{dataProviderOptionsDict[key].ServiceType}Options not found.");
                    }
                }

                return services;
            }

            public static IServiceCollection AddDataProviderServiceGroup(
                 this IServiceCollection services, IConfiguration config)
            {
                Dictionary<string, DataProviderOptions> dataProviderOptionsDict = config.GetDataProviderServiceOptions();

                //List of names of all registered Data Providers.
                var serviceConfigNames = dataProviderOptionsDict.Select(s => s.Key).ToList();

                //Get all DataProviders from Services assembly.
                var assemName = typeof(IAgentDataProvider).Assembly
                    .GetExportedTypes()
                    .Where(t => t.IsClass && t.IsPublic && t.GetInterface("IAgentDataProvider")?.Name == "IAgentDataProvider");

                //Register DataProvider implementations.
                foreach (var type in typeof(IAgentDataProvider).Assembly!.GetTypesAssignableFrom<IAgentDataProvider>())
                {
                    if (serviceConfigNames.Contains(type.Name))
                    {
                        services.AddKeyedScoped(typeof(IAgentDataProvider), type.Name, type);
                    }
                }
                
                /*
                //Delete this if not needed.
                if (assemName != null)
                {
                    // Register as services all types that implement IAgentDataProvider interface
                    foreach (Type type in assemName)
                    {
                        if (serviceConfigNames.Contains(type.Name))
                        {
                            services.AddKeyedScoped(typeof(IAgentDataProvider), type.Name, type);
                        }
                    }
                }
                */

                return services;
            }
        }
    }
}
 