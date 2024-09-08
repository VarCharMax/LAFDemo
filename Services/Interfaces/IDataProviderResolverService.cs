using LAF.Services.DataProviders.Interfaces;

namespace LAF
{
    namespace Models.Interfaces.Services
    {
        public interface IDataProviderResolverService : IDisposable
        {
            public IAgentDataProvider GetDataProvider();
        }
    }
}
