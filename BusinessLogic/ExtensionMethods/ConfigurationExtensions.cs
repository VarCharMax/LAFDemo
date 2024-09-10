using LAF.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LAF.BusinessLogic.ExtensionMethods
{
    public static class ConfigurationExtensions
    {
        public static Dictionary<string, DataProviderOptions> GetDataProviderServiceOptions(
                 this IConfiguration config)
        {
            Dictionary<string, DataProviderOptions> dataProviderOptionsDict = [];

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

            return dataProviderOptionsDict;
        }
    }
}
