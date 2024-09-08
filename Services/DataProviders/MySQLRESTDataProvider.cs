using LAF.Models.BusinessObjects;
using LAF.Models.Config;
using LAF.Services.DataProviders.Interfaces;
using LAF.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace LAF
{
    namespace Services.DataProviders
    {
        public class MySQLRESTDataProvider  : IAgentDataProvider
        {
            private bool disposedValue;
            private readonly string serviceName = "MySQLRESTDataProvider";
            private readonly DataProviderOptions _config;
            private readonly string urlService = "";
            private readonly IHttpRESTProvider _httpRESTProvider;

            public MySQLRESTDataProvider(IHttpRESTProvider restProvider, IOptions<DataProviderOptions> config)
            {
                _config = config.Value;
                urlService = _config.ServiceUrl;
                _httpRESTProvider = restProvider;
            }

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
                int id = 0;
                Agent agent = null;

                using (var httpClient = new HttpClient())
                {
                    using var response = await httpClient.GetAsync($"{urlService}/api/Agent{id}");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    agent = JsonConvert.DeserializeObject<Agent>(apiResponse);
                }

                return agent;
            }

            public async Task<Agent> MatchAgentAsync(Func<Agent, int> matchFunc, MatchRequest details)
            {
                int id = 0;
                Agent agent = null;

                using (var httpClient = new HttpClient())
                {
                    using var response = await httpClient.GetAsync($"{urlService}/api/Agent{id}");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    agent = JsonConvert.DeserializeObject<Agent>(apiResponse);
                }

                return agent;
            }

            public async Task<Agent> UpdateAgentAsync(Agent agent)
            {
                Agent receivedAgent = new();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new(JsonConvert.SerializeObject(agent), Encoding.UTF8, "application/json");

                    using var response = await httpClient.PatchAsync($"{urlService}/api/Agent", content);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedAgent = JsonConvert.DeserializeObject<Agent>(apiResponse);
                }

                return receivedAgent;
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
