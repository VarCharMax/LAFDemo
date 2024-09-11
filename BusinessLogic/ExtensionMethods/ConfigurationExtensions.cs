using LAF.Models.Config;
using LAF.Services.Classes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LAF.BusinessLogic.ExtensionMethods
{
    public static class ConfigurationExtensions
    {
        //TODO: Find some way of caching dictionary so it doesn't need to be parsed every time.
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
