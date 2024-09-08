using LAF.Models.BusinessObjects;
using LAF.Services.Interfaces;
using Newtonsoft.Json;

namespace LAF
{
    namespace Services.Providers
    {
        public class HttpRESTProvider : IHttpRESTProvider
        {
            private bool disposedValue;
            private HttpClient _httpClient;

            public HttpRESTProvider()
            {
                _httpClient = new HttpClient();
            }

            public Task<Agent> GetAgentAsync()
            {
                throw new NotImplementedException();
            }

            public async Task<List<Agent>> ListAgentsAsync(string sortBy)
            {
                List<Agent> agentList = [];
                using var response = await _httpClient.GetAsync($"{urlService}/api/Agents");
                string apiResponse = await response.Content.ReadAsStringAsync();
                agentList = JsonConvert.DeserializeObject<List<Agent>>(apiResponse);

                return agentList;
            }

            public Task<List<Agent>> GetAgentsAsync()
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
