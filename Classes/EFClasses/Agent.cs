using System.ComponentModel.DataAnnotations;

namespace LAF
{
    namespace Models.EFClasses
    {
        public class Agent
        {
            public Agent() { }

            public Agent(BusinessObjects.Agent agent)
            {
                LicenseNo = agent.LicenseNo;
                Name = agent.Name;
                LocalKnowledge = agent.LocalKnowledge;
                BestOutcome = agent.BestOutcome;
                PatienceUnderstanding = agent.PatienceUnderstanding;
                TrustworthinessReliability = agent.TrustworthinessReliability;
                BusinessSize = agent.BusinessSize;
            }

            public Agent(string licenceNo, string name, int localKnowledge = 0, int bestOutcome = 0, int patienceUnderstanding = 0, int trustworthinessReliability = 0, int businessSize = 0)
            {
                LicenseNo = licenceNo;
                Name = name;
                LocalKnowledge = localKnowledge;
                BestOutcome = bestOutcome;
                PatienceUnderstanding = patienceUnderstanding;
                TrustworthinessReliability = trustworthinessReliability;
                BusinessSize = businessSize;
            }

            [Key]
            public int AgentId { get; set; }
            public string LicenseNo { get; set; }
            public string Name { get; set; }
            public int LocalKnowledge { get; set; }
            public int BestOutcome { get; set; }
            public int PatienceUnderstanding { get; set; }
            public int TrustworthinessReliability { get; set; }
            public int BusinessSize { get; set; }
        }
    }
}
