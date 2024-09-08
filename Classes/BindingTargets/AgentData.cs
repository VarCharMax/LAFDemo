using LAF.Models.BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace LAF
{
    namespace Models.BindingTargets
    {
        public class AgentData
        {
            [Required]
            public string FirstName { get => Agent.Name; set => Agent.Name = value; }
            [Required]
            public string LicenseNo { get => Agent.LicenseNo; set => Agent.LicenseNo = value; }

            public Agent Agent { get; set; } = new Agent();
        }
    }
}

