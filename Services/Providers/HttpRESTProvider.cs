using LAF.Models.BusinessObjects;
using LAF.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace LAF
{
    namespace Services.Providers
    {
        public class HttpRESTProvider : IHttpRESTProvider
        {
            private bool disposedValue;

            public async Task<List<Agent>> ListAgentsAsync(string urlService)
            {
                List<Agent> agentList = [];
                using (var httpClient = new HttpClient())
                {
                    using var response = await httpClient.GetAsync($"{urlService}/api/Agents");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    agentList = JsonConvert.DeserializeObject<List<Agent>>(apiResponse);
                }

                return agentList;
            }

            public async Task<List<Agent>> ListAgentsAsync(string urlService, string sortBy)
            {
                List<Agent> agentList = [];
                using (var httpClient = new HttpClient())
                {
                    using var response = await httpClient.GetAsync($"{urlService}/api/Agents");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    agentList = JsonConvert.DeserializeObject<List<Agent>>(apiResponse);
                }

                return agentList;
            }

            public Task<Agent> MatchAgentAsync(string urlService, MatchRequest details)
            {
                throw new NotImplementedException();
            }

            public Task<Agent> UpdateAgentAsync(string urlService, Agent agent)
            {
                throw new NotImplementedException();
            }

            public Task<bool> DeleteAgentAsync(string urlService, string licenceNo)
            {
                throw new NotImplementedException();
            }

            public async Task<Agent> AddAgentAsync(string urlService, Agent newAgent)
            {
                Agent receivedAgent = new();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new(JsonConvert.SerializeObject(newAgent), Encoding.UTF8, "application/json");

                    using var response = await httpClient.PostAsync($"{urlService}/api/Agent", content);
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
            // ~HttpRESTProvider()
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
