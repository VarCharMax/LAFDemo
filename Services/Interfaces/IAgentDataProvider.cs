using LAF.Models.BusinessObjects;

namespace LAF
{
    namespace Services.DataProviders.Interfaces
    {
        public interface IAgentDataProvider: IDisposable
        {
            public string ServiceName { get; }
            public Task<List<Agent>> ListAgentsAsync();
            public Task<List<Agent>> ListAgentsAsync(string sortBy);
            public Task<Agent> MatchAgentAsync(MatchRequest details);
            public Task<Agent> MatchAgentAsync(Func<Agent, int> matchFunc, MatchRequest details);
            public Task<Agent> AddAgentAsync(Agent agent);
            public Task<Agent> UpdateAgentAsync(Agent agent);
            public Task<bool> DeleteAgentAsync(string licenceNo);

        }
    }
}
