using LAF.Models.BusinessObjects;
using LAF.Services.Classes;
using LAF.Services.DataProviders.Interfaces;

namespace LAF
{
    namespace Services.DataProviders
    {
        public interface IMySQLgRPCDataProviderOptions : IDataProviderOptions;

        public class MySQLgRPCDataProviderOptions : IMySQLgRPCDataProviderOptions
        {
            public MySQLgRPCDataProviderOptions(DataProviderOptions options)
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

        public class MySQLgRPCDataProvider: IAgentDataProvider
        {
            private string urlService;
            private MySQLgRPCDataProviderOptions _config;
            private bool disposedValue;
            private string serviceName = "MySQLgRPCDataProvider";

            public MySQLgRPCDataProvider(IMySQLgRPCDataProviderOptions config)
            {
                urlService = config.ServiceUrl;
            }

            string IAgentDataProvider.ServiceName { get => serviceName; }

            public Agent AddAgent(Agent agent)
            {
                Agent receivedAgent = new();

                throw new NotImplementedException();
            }

            public Task<Agent> AddAgentAsync(Agent agent)
            {
                throw new NotImplementedException();
            }

            public bool DeleteAgent(string licenceNo)
            {
                throw new NotImplementedException();
            }

            public Task<bool> DeleteAgentAsync(string licenceNo)
            {
                throw new NotImplementedException();
            }

            public List<Agent> ListAgents()
            {
                throw new NotImplementedException();
            }

            public List<Agent> ListAgents(string sortBy)
            {
                throw new NotImplementedException();
            }

            public Task<List<Agent>> ListAgentsAsync()
            {
                throw new NotImplementedException();
            }

            public Task<List<Agent>> ListAgentsAsync(string sortBy)
            {
                throw new NotImplementedException();
            }

            public Agent MatchAgent(MatchRequest details)
            {
                throw new NotImplementedException();
            }

            public Agent MatchAgent(Func<Agent, int> matchFunc, MatchRequest details)
            {
                throw new NotImplementedException();
            }

            public Task<Agent> MatchAgentAsync(MatchRequest details)
            {
                throw new NotImplementedException();
            }

            public Task<Agent> MatchAgentAsync(Func<Agent, int> matchFunc, MatchRequest details)
            {
                throw new NotImplementedException();
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
            // ~MySQLgRPCDataProvider()
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
