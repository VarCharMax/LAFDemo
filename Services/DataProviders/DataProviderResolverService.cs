using LAF.Models.Config;
using LAF.Models.Interfaces.Services;
using LAF.Services.DataProviders.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LAF
{
    /*
    namespace Services.DataProviders
    {
        // Not async because nothing it calls is async.
        public class DataProviderResolverService : IDataProviderResolverService
        {
            private bool disposedValue;
            private readonly IServiceProvider _serviceProvider;

            public DataProviderResolverService(IServiceProvider provider)
            {
                _serviceProvider = provider;
            }

            public IAgentDataProvider GetDataProvider()
            {
                IAgentDataProvider service;
                IConfiguration config = _serviceProvider.GetService<IConfiguration>();

                List<DataProviderOptions> providersOptions = [];


                var configTop = config.GetSection(ServiceConfigurationOptions.ServiceConfiguration);
                var providers = configTop.GetSection(DataProvidersOptions.DataProviders).GetChildren();

                

                string serviceName = providersOptions.Where(p => p.Default == true).First().ServiceType;

                service = _serviceProvider.GetKeyedService<IAgentDataProvider>(serviceName);

                return service;
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects)
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                    // TODO: set large fields to null
                    disposedValue = true;
                }
            }

            // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
            // ~DataProviderResolverService()
            // {
            //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
    */
}
