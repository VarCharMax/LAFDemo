using LAF.BusinessLogic.ExtensionMethods;
using LAF.Models.BusinessObjects;
using LAF.Models.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Models.BindingTargets;
using Newtonsoft.Json;

namespace LAF
{
    /*
     * This is intended for use with Fullstack clients.
     * BindingTarget classes are designed to bind with fs inputs.
     */
    namespace API.Controllers
    {
        [ApiController]
        [Route("[api/controller]")]
        public class AgentApiController : ControllerBase
        {
            private readonly IAgentDataProvider? dataService;

            public AgentApiController(WebApplication app)
            {
                var service = app.ResolveDataProviderService();

                if (service != null)
                {
                    dataService = service;
                }
                else
                {
                    throw new NullReferenceException("DataProvider service not found.");
                }
            }

            [HttpGet]
            public string Get()
            {
                return "Agent Matcher Service is running!";
            }

            [HttpPost("match")]
            public async Task<ActionResult<List<Agent>>> MatchAgent([FromBody] MatchRequest request)
            {
                int requestCount = 0;

                if (request == null || request.SizePreference <= 0 || string.IsNullOrEmpty(request.AttributePreference))
                {
                    return BadRequest("Invalid request parameters");
                }

                var matchedAgent = await dataService!.MatchAgentAsync(request);


                if (matchedAgent != null)
                {
                    if (requestCount % 100 == 0)
                    {

                    }
                    return Ok(JsonConvert.SerializeObject(matchedAgent));
                }
                else
                {
                    return NotFound("No matching agent found");
                }

            }

            [HttpGet("agents")]
            public async Task<ActionResult<List<Agent>>> GetAllAgents(string sortBy)
            {
                var agents = await dataService!.ListAgentsAsync(sortBy);

                return Ok(agents);
            }

            // POST api/<AgentController>
            [HttpPost]
            public async Task<ActionResult<int>> AddAgent([FromBody] AgentData cdata)
            {
                if (ModelState.IsValid)
                {
                    Agent a = cdata.Agent;

                    await dataService!.AddAgentAsync(a);

                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
        }
    }
}