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
                services.Configure<ServiceConfigurationOptions>(
                   config.GetSection(ServiceConfigurationOptions.ServiceConfiguration));

                /*
                var dataProviderList = services.AsEnumerable().Where(s => s is IAgentDataProvider).ToList();
                // var servicesProvider = scope.ServiceProvider;

                var topConfig = config.GetSection(ServiceConfigurationOptions.ServiceConfiguration);

                

                services.AddOptions<DataProviderOptions>();

                services.Configure<ServiceConfigurationOptions>(
                    config.GetSection(ServiceConfigurationOptions.ServiceConfiguration));

                var providers = config.GetSection(DataProviderOptions.DataProvider).Get<DataProviderOptions>();

                var service1 = new MySQLRESTDataProvider(config);

                services.AddOptions<DataProviderOptions>()
                .Configure<MySQLRESTDataProvider>(
                        (o, s) => o.ServiceUrl = "127.0.0.1");

            
                    services.Configure<ServiceConfigurationSettings>(config.GetSection(ServiceConfigurationSettings.DataServiceProviders.));
                */

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
                            Type? interfaceService = type.GetInterface("IAgentDataProvider");

                            DataProviderOptions? param = dataProviderOptionsDict.GetValueOrDefault(type.Name);

                            if (param != null)
                            {
                                IAgentDataProvider? dProv = (IAgentDataProvider?)Activator.CreateInstance(type, param);

                                if (dProv != null)
                                {
                                    //Dummy object to satisy IServiceProvider parameter.
                                    var obj1 = new object();

                                    // services.AddScoped(type.GetInterface("IAgentDataProvider")!, type);

                                    //Add Keyed service.
                                    // TODO: Might not work becase of dummy ServiceProvider.
                                    // services.Add(new ServiceDescriptor(interfaceService!, dProv.ServiceName, (obj1, dProv) => { return dProv!; }, ServiceLifetime.Scoped));
                                }
                            }
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
 