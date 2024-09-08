using LAF.Models.EFClasses;

namespace LAF
{
    namespace Models.Interfaces.Database
    {
        public interface IAgentRepository
        {
            public Task<List<Agent>> GetAgentsAsync();
            public Task<List<Agent>> GetAgentsAsync(string sortBy);
            public Task<Agent> MatchAgentAsync(string licenceNo);
            public Task<bool> AddAgentAsync(Models.BusinessObjects.Agent agent);
            public Task<int> SaveChangesAsync();
        }
    }
}
