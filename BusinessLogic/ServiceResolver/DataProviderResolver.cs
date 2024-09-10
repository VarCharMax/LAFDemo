using LAF.BusinessLogic.ExtensionMethods;
using LAF.Models.Interfaces.Services;
using LAF.Services.DataProviders.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LAF.BusinessLogic.ServiceResolver
{
    public class DataServiceResolver(IServiceProvider services, IConfiguration config) : IDataProviderResolverService
    {
        public IAgentDataProvider? GetProviderService()
        {
            IAgentDataProvider? service;

            var serviceList = config.GetDataProviderServiceOptions().Select(s => s.Value).ToList();

            string serviceName = serviceList.Where(p => p.Default == true).First().ServiceType;

            return service = services.GetKeyedService<IAgentDataProvider>(serviceName);
        }
    }
}

