using LAF.Models.EFClasses;
using LAF.Models.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace LAF
{
    namespace MySQLDatastore
    {
        public class AgentMySQLRepository : IAgentRepository, IDisposable
        {
            private readonly MySQLDbContext context;
            private bool disposedValue;

            public AgentMySQLRepository(MySQLDbContext ctx)
            {
                context = ctx;
            }

            public async Task<bool> AddAgentAsync(Models.BusinessObjects.Agent agent)
            {
                await context.Agents.AddAsync(new Agent(agent));

                return true;
            }

            public async Task<List<Agent>> GetAgentsAsync()
            {
                return await context.Agents.ToListAsync();
            }

            public async Task<List<Agent>> GetAgentsAsync(string sortBy)
            {
                return await context.Agents.ToListAsync();
            }

            public async Task<Agent> MatchAgentAsync(string licenceNo)
            {
                return await context.Agents.FindAsync(licenceNo);
            }

            public async Task<int> SaveChangesAsync()
            {
                return await context.SaveChangesAsync();
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
            // ~AgentMySQLRepository()
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
