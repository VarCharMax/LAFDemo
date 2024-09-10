using LAF.Services.DataProviders.Interfaces;

namespace LAF
{
    namespace Models.Interfaces.Services
    {
        public interface IDataProviderResolverService
        {
            public IAgentDataProvider GetProviderService();
        }
    }
}
