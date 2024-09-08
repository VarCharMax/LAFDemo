namespace LAF
{
    namespace Models.BusinessObjects
    {
        public class Agent
        {
            public Agent() { }

            public Agent(EFClasses.Agent dbAgent)
            {

                Id = dbAgent.AgentId;
                LicenseNo = dbAgent.LicenseNo;
                Name = dbAgent.Name;
                LocalKnowledge = dbAgent.LocalKnowledge;
                BestOutcome = dbAgent.BestOutcome;
                PatienceUnderstanding = dbAgent.PatienceUnderstanding;
                TrustworthinessReliability = dbAgent.TrustworthinessReliability;
                BusinessSize = dbAgent.BusinessSize;
            }

            public int Id { get; set; }
            public string LicenseNo { get; set; } = "";
            public string Name { get; set; } = "";
            public int LocalKnowledge { get; set; }
            public int BestOutcome { get; set; }
            public int PatienceUnderstanding { get; set; }
            public int TrustworthinessReliability { get; set; }
            public int BusinessSize { get; set; }
        }
    }
}
