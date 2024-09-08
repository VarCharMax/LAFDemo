using LAF.Models.BusinessObjects;
using LAF.Models.Config;
using LAF.Models.Interfaces.Services;
using LAF.MVC.Controllers;
using LAF.Services.DataProviders;
using LAF.Services.DataProviders.Interfaces;
using LAF.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;

namespace LAF
{
    namespace ServiceUnitTests
    {
        public class APITests
        {
            [Fact]
            public async Task APIReturnsDataAsync()
            {
                // Create mock for service dependency
                var mockAgentService = new Mock<IAgentDataProvider>();
                var mockHttpService = new Mock<IHttpRESTProvider>();
                var mockOptionsService = new Mock<IHttpRESTProvider>();
                var mockResolverService = new Mock<IDataProviderResolverService>();
                var mockConfigService = new Mock<IConfiguration>();
                var dataProviderOptions = new DataProviderOptions();
                var matchRequest = new MatchRequest();

                string mockUrl = "127.0.0.1";
                var httpType = mockHttpService.Setup(p => p.MatchAgentAsync(mockUrl, new MatchRequest())).Returns(Task.FromResult(new Agent { LicenseNo = "1234", Name = "Dorian Gray" }));

                var agentResolveType = mockResolverService.Setup(p => p.GetDataProvider()).Returns(new MySQLRESTDataProvider(mockHttpService.Object, (IOptions<MySQLRESTDataProviderOptions>)dataProviderOptions));

                //This is how to mock an asynchronous method.
                var agentType = mockAgentService.Setup(p => new MySQLRESTDataProvider(mockHttpService.Object, (IOptions<MySQLRESTDataProviderOptions>)dataProviderOptions));

                // mockAgentService.Object.MatchAgentAsync()

                //Testing /api/agent/1 endpoint.
                var controller = new AgentController(mockResolverService.Object);

                //Test that api call returns a successful ActionResult
                var model = await controller.MatchAgentAsync(matchRequest) as OkObjectResult;

                Assert.NotNull(model);

                //Test that object embeded in ActionResult is correct type.
                var result = model.Value as Agent;

                Assert.NotNull(result);
                
            }
        }
    }
}