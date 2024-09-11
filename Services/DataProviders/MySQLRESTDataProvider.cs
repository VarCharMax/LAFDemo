using LAF.Models.BusinessObjects;
using LAF.Services.Classes;
using LAF.Services.DataProviders.Interfaces;
using LAF.Services.Interfaces;

namespace LAF
{
    namespace Services.DataProviders
    {
        public interface IMySQLRESTDataProviderOptions : IDataProviderOptions;

        public class MySQLRESTDataProviderOptions : IMySQLRESTDataProviderOptions
        {
            public MySQLRESTDataProviderOptions(DataProviderOptions options)
            {
                this.ServiceType = options.ServiceType;
                this.Default = options.Default;
                this.ServiceUrl = options.ServiceUrl;
            }

            public string ServiceType { get; set; }
            public string ServiceUrl { get; set; }
            public bool Default { get; set; }

            public void Dispose()
            {
                
            }
        }

        public class MySQLRESTDataProvider(IHttpRESTProvider restProvider, IMySQLRESTDataProviderOptions config) : IAgentDataProvider
        {
            private bool disposedValue;
            private readonly string serviceName = "MySQLRESTDataProvider";
            private readonly string urlService = config.ServiceUrl;
            private readonly IHttpRESTProvider _httpRESTProvider = restProvider;

            public string ServiceName { get => serviceName; }

            public async Task<Agent> AddAgentAsync(Agent newAgent)
            {
                return await _httpRESTProvider.AddAgentAsync(urlService, newAgent);
            }

            public async Task<bool> DeleteAgentAsync(string urlService, string licenceNo)
            {
                return await Task.Run(() =>
                {
                    return true;
                });
            }

            public async Task<List<Agent>> ListAgentsAsync(string urlService)
            {
                return await _httpRESTProvider.ListAgentsAsync(urlService);
            }

            public async Task<List<Agent>> ListAgentsAsync(string urlService, string sortBy)
            {
                return await _httpRESTProvider.ListAgentsAsync(urlService, sortBy);
            }

            public async Task<Agent> MatchAgentAsync(MatchRequest details)
            {
                return await _httpRESTProvider.MatchAgentAsync(urlService, details);
            }

            public async Task<Agent> MatchAgentAsync(Func<Agent, int> matchFunc, MatchRequest details)
            {
                return await _httpRESTProvider.MatchAgentAsync(urlService, details);
            }

            public async Task<Agent> UpdateAgentAsync(Agent agent)
            {
                return await _httpRESTProvider.UpdateAgentAsync(urlService, agent);
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
            // ~MySQLRESTDataProvider()
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

            public Task<List<Agent>> ListAgentsAsync()
            {
                throw new NotImplementedException();
            }

            public Task<bool> DeleteAgentAsync(string licenceNo)
            {
                throw new NotImplementedException();
            }
        }
    }
}
