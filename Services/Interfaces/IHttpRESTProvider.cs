using LAF.Models.BusinessObjects;

namespace LAF.Services.Interfaces
{
    public interface IHttpRESTProvider: IDisposable
    {
        public Task<Agent> GetAgentAsync();
        public Task<List<Agent>> GetAgentsAsync();
        public Task<List<Agent>> ListAgentsAsync(string sortBy);

    }
}
