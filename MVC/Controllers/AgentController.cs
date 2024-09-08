using LAF.Models.BusinessObjects;
using LAF.Models.Interfaces.Services;
using LAF.Services.DataProviders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LAF
{
    namespace MVC.Controllers
    {
        public class AgentController : Controller
        {
            private readonly IAgentDataProvider? dataService;

            public AgentController(IDataProviderResolverService dataProviderService)
            {
                // var service = app.ResolveDataProviderService();

                if (dataProviderService != null)
                {
                    dataService = dataProviderService.GetDataProvider();
                }
                else
                {
                    throw new NullReferenceException("DataProvider service not found.");
                }
            }

            // GET: AgentController
            public ActionResult Index()
            {

                return View();
            }

            // GET: AgentController/MatchAgent/5
            public async Task<ActionResult> MatchAgentAsync([FromBody] MatchRequest request)
            {
                int requestCount = 0;

                if (request == null || request.SizePreference <= 0 || string.IsNullOrEmpty(request.AttributePreference))
                {
                    return View();
                }

                try
                {
                    var matchedAgent = await dataService!.MatchAgentAsync(request);

                    if (matchedAgent != null)
                    {
                        if (requestCount % 100 == 0)
                        {

                        }
                        return View(matchedAgent);
                    }
                    else
                    {
                        return View();
                    }

                }
                catch (Exception)
                {
                    return View();
                }
            }

            // GET: AgentController/ListAgents/{sortBy}
            public async Task<ActionResult> ListAgentsAsync(string sortBy = "")
            {
                var agents = await dataService!.ListAgentsAsync(sortBy);

                return View(agents);
            }

            // POST: AgentController/AddAgent
            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<IActionResult> AddAgentAsync([FromBody] Agent newAgent)
            {
                if (ModelState.IsValid)
                {
                    Agent a = newAgent;

                    try
                    {
                        await dataService!.AddAgentAsync(a);

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        return View();
                    }
                }
                else
                {
                    return View(ModelState);
                }
            }

            // GET: AgentController/Edit/5
            public ActionResult Edit(int id)
            {
                return View();
            }

            // POST: AgentController/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, IFormCollection collection)
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            // GET: AgentController/Delete/5
            public ActionResult Delete(int id)
            {
                return View();
            }

            // POST: AgentController/Delete/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Delete(int id, IFormCollection collection)
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }
    }
}
