using LAF.Models.BusinessObjects;

namespace LAF.Services.Interfaces
{
    public interface IHttpRESTProvider: IDisposable
    {
        public Task<Agent> MatchAgentAsync(string urlService);
        public Task<List<Agent>> ListAgentsAsync(string urlService);
        public Task<List<Agent>> ListAgentsAsync(string urlService, string sortBy);
        public Task<Agent> AddAgentAsync(string urlService, Agent newAgent);
        public Task<Agent> MatchAgentAsync(string urlService, MatchRequest details);
        public Task<Agent> UpdateAgentAsync(string urlService, Agent agent);
        public Task<bool> DeleteAgentAsync(string urlService, string licenceNo);
    }
}
