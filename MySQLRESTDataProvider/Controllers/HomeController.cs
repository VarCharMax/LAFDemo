using LAF.Models.BusinessObjects;
using LAF.MySQLDatastore;
using Microsoft.AspNetCore.Mvc;

namespace LAF
{
    namespace MySQLRESTDataProvider.Controllers
    {
        [Route("api/[controller]")]
        public class HomeController : Controller
        {
            private readonly AgentMySQLRepository context;

            public HomeController(AgentMySQLRepository ctx)
            {
                context = ctx;
            }

            [HttpPost("match")]
            public async Task<ActionResult<Agent>> MatchAgent(MatchRequest request)
            {
                return await Task.Run(() =>
                {
                    return Ok(new Agent());
                });
            }

            [HttpGet("agents{sortby}")]
            public async Task<ActionResult<List<Agent>>> GetAllAgents(string sortby)
            {
                try
                {
                    List<LAF.Models.EFClasses.Agent> dbAgents = await context.GetAgentsAsync();
                    List<Agent> agents = [];
                    dbAgents.ForEach((a) =>
                    {
                        agents.Add(new Agent(a));
                    });

                    return Ok(agents);
                }
                catch (Exception)
                {
                    return BadRequest("Error");
                }
            }

            [HttpGet("agents")]
            public async Task<ActionResult<List<Agent>>> GetAllAgents()
            {
                List<Agent> agents = [];
                try
                {
                    List<LAF.Models.EFClasses.Agent> dbAgents = await context.GetAgentsAsync();
                    dbAgents.ForEach((a) =>
                    {
                        agents.Add(new Agent(a));
                    });

                    return Ok(agents);
                }
                catch (Exception)
                {
                    return BadRequest("Error");
                }
            }

            [HttpPost("add")]
            public async Task<IActionResult> AddAgent(Agent newAgent)
            {
                await context.AddAgentAsync(newAgent);
                await context.SaveChangesAsync();

                return Ok();
            }
        }
    }
}
