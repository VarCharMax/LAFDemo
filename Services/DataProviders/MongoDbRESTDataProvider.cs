using LAF.Models.BusinessObjects;
using LAF.Models.Config;
using LAF.Services.DataProviders.Interfaces;
using LAF.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace LAF
{
    namespace Services.DataProviders
    {
        // A skeleton just to show that we can swap data services by changing the service provider.
        public class MongoDbRESTDataProvider: IAgentDataProvider
        {
            private DataProviderOptions _config;
            private string serviceUrl;
            private string serviceName = "MongoDbRESTDataProvider";

            public MongoDbRESTDataProvider(IHttpRESTProvider restProvider, IOptions<DataProviderOptions> config)
            {
                _config = config.Value;
                serviceUrl = _config.ServiceUrl;
            }

            private readonly List<Agent> agents =
                        [
                            new Agent { Id = 1, LicenseNo = "4235456", Name = "John Smith", LocalKnowledge = 8, BestOutcome = 9, PatienceUnderstanding = 7, TrustworthinessReliability = 8, BusinessSize = 10 },
                        new Agent { Id = 2, LicenseNo = "56543", Name = "Jane Doe", LocalKnowledge = 7, BestOutcome = 8, PatienceUnderstanding = 8, TrustworthinessReliability = 7, BusinessSize = 20 },
                        new Agent { Id = 3, LicenseNo = "7654", Name = "Bob Johnson", LocalKnowledge = 9, BestOutcome = 10, PatienceUnderstanding = 8, TrustworthinessReliability = 9, BusinessSize = 30 },
                        new Agent { Id = 4, LicenseNo = "098765", Name = "Mike Williams", LocalKnowledge = 6, BestOutcome = 7, PatienceUnderstanding = 7, TrustworthinessReliability = 6, BusinessSize = 20 },
                        new Agent { Id = 5, LicenseNo = "34876", Name = "Emma Johnson", LocalKnowledge = 10, BestOutcome = 9, PatienceUnderstanding = 10, TrustworthinessReliability = 10, BusinessSize = 10 },
                    ];
            private bool disposedValue;

            

            string IAgentDataProvider.ServiceName { get => serviceName; }

            public async Task<Agent> AddAgentAsync(Agent newAgent)
            {
                return await Task.Run(() =>
                {
                    return new Agent();
                });
            }

            public async Task<bool> DeleteAgentAsync(string licenceNo)
            {
                return await Task.Run(() =>
                {
                    return true;
                });
            }

            public async Task<List<Agent>> ListAgentsAsync()
            {
                return await Task.Run(() =>
                {
                    return agents;
                });
            }

            public async Task<List<Agent>> ListAgentsAsync(string sortBy)
            {
                return await Task.Run(() =>
                {
                    return agents;
                });
            }

            public async Task<Agent> MatchAgentAsync(MatchRequest details)
            {
                return await Task.Run(() =>
                {
                    return new Agent();
                });
            }

            public Agent MatchAgent(Func<Agent, int> matchFunc, MatchRequest details)
            {
                return new Agent();
            }

            public async Task<Agent> MatchAgentAsync(Func<Agent, int> matchFunc, MatchRequest details)
            {
                return await Task.Run(() =>
                {
                    return new Agent();
                });
            }

            public Task<Agent> UpdateAgentAsync(Agent agent)
            {
                throw new NotImplementedException();
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
            // ~MongoDbRESTDataProvider()
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
}
