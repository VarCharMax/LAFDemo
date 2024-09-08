using LAF.Models.BusinessObjects;
using LAF.Models.Interfaces.Services;
using LAF.Services.DataProviders;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LAF
{
    namespace ServiceUnitTests
    {
        public class APITests
        {
            [Fact]
            public void APIReturnsDataAsync()
            {
                /*
                // Create mock for service dependency
                var mockAgentService = new Mock<IAgentDataProvider>();

                //This is how to mock an asynchronous method.
                var agentType = mockAgentService.Setup(p => p.MatchAgentAsync(new MatchRequest())).Returns(Task.FromResult(new Agent { LicenseNo = "1234", Name = "Dorian Gray" }));

                var mockResolverService = new Mock<IDataProviderResolverService>();
                var agentResolveType = mockResolverService.Setup(p => p.GetDataProvider()).Returns(new MySQLRESTDataProvider());

                //Testing /api/agent/1 endpoint.
                var controller = new AbsDataController(mockService.Object);

                //Test that api call returns a successful ActionResult
                var model = await controller.GetData(104, 1) as OkObjectResult;

                Assert.NotNull(model);

                //Test that object embeded in ActionResult is correct type.
                var result = model.Value as Agent;

                Assert.NotNull(result);
                */
            }
        }
    }
}