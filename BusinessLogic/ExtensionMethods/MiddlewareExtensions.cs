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
                var configTop = config.GetSection(ServiceConfigurationOptions.ServiceConfiguration);
                var providers = configTop.GetSection(DataProvidersOptions.DataProviders).GetChildren();

                //TODO: Complete auto type creation.
                foreach (var item in providers)
                {
                    var prov = item.GetRequiredSection("DataProvider").Get<DataProviderOptions>();

                    if (prov != null)
                    {
                        // dataProviderOptionsDict.Add(prov.ServiceType, prov);

                        services.Configure<MySQLRESTDataProviderOptions>(
                            config.GetSection(DataProvidersOptions.DataProviders));
                    }
                }

                return services;
            }

            public static IServiceCollection AddDataProviderServiceGroup(
                 this IServiceCollection services, IConfiguration config)
            {
                
                var configTop = config.GetSection(ServiceConfigurationOptions.ServiceConfiguration);
                var providers = configTop.GetSection(DataProvidersOptions.DataProviders).GetChildren();

                Dictionary<string, DataProviderOptions> dataProviderOptionsDict = [];

                // Create dictionary of registered providers.
                foreach (var item in providers)
                {
                    var prov = item.GetRequiredSection("DataProvider").Get<DataProviderOptions>();

                    if (prov != null)
                    {
                        dataProviderOptionsDict.Add(prov.ServiceType, prov);
                    }
                }
                 
                //List of names of all registered Data Providers.
                var serviceConfigNames = dataProviderOptionsDict.Select(s => s.Key).ToList();

                //Get all DataProviders from Services assembly.
                var assemName = typeof(IAgentDataProvider).Assembly
                    .GetExportedTypes()
                    .Where(t => t.IsClass && t.IsPublic && t.GetInterface("IAgentDataProvider")?.Name == "IAgentDataProvider");
                
                if (assemName != null)
                {
                    // Register as services all types that implement IAgentDataProvider interface
                    foreach (Type type in assemName)
                    {
                        if (serviceConfigNames.Contains(type.Name))
                        {
                            services.AddScoped(typeof(IAgentDataProvider), type);
                        }
                    }
                }

                services.AddScoped<IHttpRESTProvider, HttpRESTProvider>();
                services.AddScoped<IDataProviderResolverService, DataProviderResolverService>();

                return services;
            }
            
            
            public static IAgentDataProvider? ResolveDataProviderService(
                this WebApplication app)
            {
                IAgentDataProvider? service;
                IConfiguration config = app.Configuration;

                List<DataProviderOptions> providersOptions = [];

                var configTop = config.GetSection(ServiceConfigurationOptions.ServiceConfiguration);
                configTop.GetSection(DataProvidersOptions.DataProviders)
                    .GetChildren()
                    .ToList()
                    .ForEach(s => { providersOptions.Add(s.GetRequiredSection("DataProvider").Get<DataProviderOptions>()!); });

                string serviceName = providersOptions.Where(p => p.Default == true).First().ServiceType;
                
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                service = services.GetKeyedService<IAgentDataProvider>(serviceName);

                return service;
            }
            
        }
    }
}
 