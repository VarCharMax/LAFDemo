using LAF.Models.BusinessObjects;

namespace LAF
{
    namespace BusinessLogic.Methods
    {
        public class BusinessLogic
        {
            public static int GetScore(Agent agent, string attribute)
            {
                //TODO: Try to find a way of automating this so we don't have to remember to add new conditions to it.
                return attribute switch
                {
                    "localknowledge" => agent.LocalKnowledge,
                    "bestoutcome" => agent.BestOutcome,
                    "patienceunderstanding" => agent.PatienceUnderstanding,
                    "trustworthinessreliability" => agent.TrustworthinessReliability,
                    _ => 0,
                };
            }
        }
    }
}
