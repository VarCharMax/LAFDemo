using LAF.BusinessLogic.ServiceResolver;
using LAF.Middleware;
using LAF.Models.Config;
using LAF.Models.Interfaces.Services;
using LAF.Services.DataProviders;
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

                /*
                config.GetSection(ServiceConfigurationOptions.ServiceConfiguration)
                    .GetSection(DataProvidersOptions.DataProviders)
                    .GetChildren()
                    .ToList()
                    .ForEach(g => {
                        var opts = g.GetRequiredSection("DataProvider");
                        DataProviderOptions pOptions = new();
                        opts.Bind(pOptions); 
                        dataProviderOptionsDict.Add(pOptions.ServiceType, pOptions); 
                    });
                */

                //Add DataProviderOptions to ServiceProvider for each registered dataProvider type.
                foreach (var key in dataProviderOptionsDict.Keys)
                {
                    Type? t = Type.GetType($"LAF.Services.DataProviders.{dataProviderOptionsDict[key].ServiceType}Options, LAF.Services");

                    if (t != null)
                    {
                        DataProviderOptions options = dataProviderOptionsDict[key];

                        if (Activator.CreateInstance(t, options) is IDataProviderOptions provider)
                        {
                            //Since we cannot specify generics at runtime, we cannot use the IOptions pattern.
                            //It looks like this is the only way of registering the data options subclasses as services.
                            //But these aren't managed services - they need to be disposed.
                            // services.Add(new ServiceDescriptor(typeof(IAgentDataProvider), provider, ServiceLifetime.Scoped));

                            //Or ... (only use one!)
                            services.AddScoped<IDataProviderOptions>(implementationFactory: sProvider => { return provider; });
                        }
                    }
                }

                return services;
            }

            public static IServiceCollection AddDataProviderServiceGroup(
                 this IServiceCollection services, IConfiguration config)
            {
                Dictionary<string, DataProviderOptions> dataProviderOptionsDict = config.GetDataProviderServiceOptions();

                /*
                config.GetSection(ServiceConfigurationOptions.ServiceConfiguration)
                   .GetSection(DataProvidersOptions.DataProviders)
                   .GetChildren()
                   .ToList()
                   .ForEach(g => {
                       var opts = g.GetRequiredSection("DataProvider");
                       DataProviderOptions pOptions = new();
                       opts.Bind(pOptions);
                       dataProviderOptionsDict.Add(pOptions.ServiceType, pOptions);
                   });
                */

                //List of names of all registered Data Providers.
                var serviceConfigNames = dataProviderOptionsDict.Select(s => s.Key).ToList();

                //Get all DataProviders from Services assembly.
                var assemName = typeof(IAgentDataProvider).Assembly
                    .GetExportedTypes()
                    .Where(t => t.IsClass && t.IsPublic && t.GetInterface("IAgentDataProvider")?.Name == "IAgentDataProvider");

                //TODO: Try this implementation.
                foreach (var type in typeof(IAgentDataProvider).Assembly!.GetTypesAssignableFrom<IAgentDataProvider>())
                {
                    if (serviceConfigNames.Contains(type.Name))
                    {
                        //TODO: Do we need different interfaces for specific services?
                        //You can register multiple implemntations. There is a ServiceResolver class, but not sure
                        ////if I can use it if it needs generics specified.
                        services.AddKeyedScoped(typeof(IAgentDataProvider), type.Name, type);
                    }
                }

                //Delete this if not needed.
                if (assemName != null)
                {
                    // Register as services all types that implement IAgentDataProvider interface
                    foreach (Type type in assemName)
                    {
                        if (serviceConfigNames.Contains(type.Name))
                        {
                            //TODO: Do we need different interfaces for specific services?
                            //You can register multiple implemntations. There is a ServiceResolver class, but not sure
                            ////if I can use it if it needs generics specified.
                            services.AddKeyedScoped(typeof(IAgentDataProvider), type.Name, type);
                        }
                    }
                }
                
                services.AddScoped<IHttpRESTProvider, HttpRESTProvider>();
                services.AddScoped<IDataProviderResolverService, DataServiceResolver>();

                return services;
            }
        }
    }
}
 