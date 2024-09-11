using LAF.BusinessLogic.ServiceResolver;
using LAF.Models.BusinessObjects;
using LAF.Models.Interfaces.Services;
using LAF.MVC.Controllers;
using LAF.Services.Classes;
using LAF.Services.DataProviders;
using LAF.Services.DataProviders.Interfaces;
using LAF.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace LAF
{
    namespace ServiceUnitTests
    {
        public class ServiceTests
        {
            [Fact]
            public async Task MySQLRESTDataProviderReturnsDataAsync()
            {
                var agentTest = new Agent { LicenseNo = "1234", Name = "Dorian Gray" };
                var mockHttpService = new Mock<IHttpRESTProvider>();
                var mockResolverService = new Mock<IDataProviderResolverService>();
                var dataOptions = new MySQLRESTDataProviderOptions(new DataProviderOptions() { Default = true, ServiceType = "MySQLRESTDataProvider", ServiceUrl = "127.0.0.1" }); 
                var matchRequest = new MatchRequest { SizePreference = 6, AttributePreference = "localknowledge" };
                string mockUrl = "127.0.0.1";

                //This is how to mock an asynchronous method.
                var httpType = mockHttpService.Setup(p => p.MatchAgentAsync(mockUrl, matchRequest)).Returns(Task.FromResult(agentTest));
                var agentResolveType = mockResolverService.Setup(p => p.GetProviderService()).Returns(new MySQLRESTDataProvider(mockHttpService.Object, dataOptions));

                //Testing AgentController/MatchAgent/5 endpoint.
                var controller = new AgentController(mockResolverService.Object);

                //Test that api call returns a successful ViewResult
                var viewResult = await controller.MatchAgentAsync(matchRequest) as ViewResult;

                Assert.NotNull(viewResult);

                //Test that object embeded in ViewResult is correct type.
                var result = viewResult.ViewData.Model as Agent;

                Assert.NotNull(result);
            }

            [Fact]
            public async Task ServicesReturnDataAsync()
            {
                /*
                 * Testing all data services using minimal mocks.
                 * All services are real except for HttpProvider.
                 * Should probably be called after all services have been individually tested, to show that they all work together.
                 */

                var agentTest = new Agent { LicenseNo = "1234", Name = "Dorian Gray" };
                var mockHttpService = new Mock<IHttpRESTProvider>();
                var matchRequest = new MatchRequest { SizePreference = 6, AttributePreference = "localknowledge" };
                string mockUrl = "127.0.0.1";

                var httpType = mockHttpService.Setup(p => p.MatchAgentAsync(mockUrl, matchRequest)).Returns(Task.FromResult(agentTest));

                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                IServiceCollection services = new ServiceCollection()
                    .AddSingleton<IConfiguration>(config)
                    .AddScoped(provider => mockHttpService.Object)
                    .AddKeyedScoped<IAgentDataProvider, MySQLRESTDataProvider>("MySQLRESTDataProvider")
                    .AddScoped<IDataProviderResolverService, DataServiceResolver>()
                    .AddScoped<IMySQLRESTDataProviderOptions>(provider => new MySQLRESTDataProviderOptions(new DataProviderOptions { Default = true, ServiceType = "MySQLRESTDataProvider", ServiceUrl = mockUrl }));

                var sp = services.BuildServiceProvider();

                var controller = new AgentController(sp.GetRequiredService<IDataProviderResolverService>());

                //Test that api call returns a successful ViewResult
                var viewResult = await controller.MatchAgentAsync(matchRequest) as ViewResult;

                Assert.NotNull(viewResult);

                //Test that object embeded in ViewResult is correct type.
                var result = viewResult.ViewData.Model as Agent;

                Assert.NotNull(result);
            }
        }
    }
}