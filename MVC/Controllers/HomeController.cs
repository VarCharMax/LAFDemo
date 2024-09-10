using LAF.Models.Interfaces.Services;
using LAF.Services.DataProviders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LAF
{
    namespace MVC.Controllers
    {
        public class HomeController : Controller
        {
            private readonly IAgentDataProvider? dataService;

            public HomeController(IDataProviderResolverService dataProviderService) {
                if (dataProviderService != null)
                {
                    dataService = dataProviderService.GetProviderService();
                }
                else
                {
                    throw new NullReferenceException("DataProvider service not found.");
                }
            }

            public async Task<IActionResult> Index()
            {
                var lst = await dataService!.ListAgentsAsync();

                return View();
            }

        }
    }
}
