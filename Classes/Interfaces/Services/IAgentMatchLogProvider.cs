namespace LAF
{
    namespace Models.Interfaces.Services
    {
        public interface IAgentMatchLogProvider: IDisposable
        {
            public Task<bool> LogMatchAgentAsync(string agentLicenceNo);
        }
    }
}
