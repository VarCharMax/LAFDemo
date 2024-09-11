﻿using LAF.BusinessLogic.ExtensionMethods;
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

            //This resolver uses the appsettings file, but the idea is that there could be a more sophisticated implementation based on a database
            //or some other instrumentation, so the service could fall back to an alternative if the preferred one degrades, etc.

            var serviceList = config.GetDataProviderServiceOptions().Select(s => s.Value).ToList();

            string serviceName = serviceList.Where(p => p.Default == true).First().ServiceType;

            return service = services.GetKeyedService<IAgentDataProvider>(serviceName);
        }
    }
}

