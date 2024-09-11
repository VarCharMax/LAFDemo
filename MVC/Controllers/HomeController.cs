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

            public HomeController(IDataProviderResolverService dataProviderResolverService) {
                if (dataProviderResolverService != null)
                {
                    dataService = dataProviderResolverService.GetProviderService();

                    if (dataService == null)
                    {
                        throw new ArgumentNullException("DataProvider service not found.");
                    }
                }
                else
                {
                    throw new NullReferenceException("DataProvider Resolver service not found.");
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
