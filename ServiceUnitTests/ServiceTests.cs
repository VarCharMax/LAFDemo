using LAF.BusinessLogic.ExtensionMethods;
using LAF.BusinessLogic.ServiceResolver;
using LAF.Models.BusinessObjects;
using LAF.Models.Interfaces.Services;
using LAF.MVC.Controllers;
using LAF.Services.Classes;
using LAF.Services.DataProviders;
using LAF.Services.DataProviders.Interfaces;
using LAF.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
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

                //This is how to mock an asynchronous method.
                var agentResolveType = mockResolverService.Setup(p => p.GetProviderService(It.IsAny<string>())).Returns(new MySQLRESTDataProvider(mockHttpService.Object, dataOptions));
                var httpType = mockHttpService.Setup(p => p.MatchAgentAsync(It.IsAny<string>(), It.IsAny<MatchRequest>())).Returns(Task.FromResult(agentTest));

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
            public void DataServicesAllLoad()
            {
                var agentTest = new Agent { LicenseNo = "1234", Name = "Dorian Gray" };
                var mockHttpService = new Mock<IHttpRESTProvider>();

                var httpType = mockHttpService.Setup(p => p.MatchAgentAsync(It.IsAny<string>(), It.IsAny<MatchRequest>())).Returns(Task.FromResult(agentTest));

                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                var builder = WebApplication.CreateBuilder();

                IServiceCollection services = new ServiceCollection()
                    .AddSingleton<IConfiguration>(config)
                    .AddScoped(provider => mockHttpService.Object)
                    .AddDataProviderServiceConfig(builder.Configuration)
                    .AddDataProviderServiceGroup(builder.Configuration);

                var serviceNames = builder.Configuration.GetDataProviderServiceOptions().Select(d => d.Key).ToList();

                var sp = services.BuildServiceProvider();

                List<IAgentDataProvider> foundServices = [];

                foreach (var item in serviceNames)
                {
                    // Looks like keyed services are not returned via GetService().
                    // Must use GetKeyedServices<IAgentDataProvider>("serviceName").

                    var svc = sp.GetKeyedService<IAgentDataProvider>(item);

                    if (svc != null)
                    {
                        foundServices.Add(svc);
                    }
                }
               
               Assert.Equal(3, foundServices.Count);

                Assert.Collection(foundServices,
                   e => Assert.IsAssignableFrom<IAgentDataProvider>(e),
                   e => Assert.IsAssignableFrom<IAgentDataProvider>(e),
                   e => Assert.IsAssignableFrom<IAgentDataProvider>(e)
                );
            }

            [Fact]
            public async Task ServicesAllReturnDataAsync()
            {
                /*
                 * Testing all data services in combination using minimal mocks.
                 * All services are real except for HttpProvider.
                 * Should probably be called after all services have been individually tested, to show that they all work together.
                 */

                var agentTest = new Agent { LicenseNo = "1234", Name = "Dorian Gray" };
                var mockHttpService = new Mock<IHttpRESTProvider>();
                var matchRequest = new MatchRequest { SizePreference = 6, AttributePreference = "localknowledge" };

                var httpType = mockHttpService.Setup(p => p.MatchAgentAsync(It.IsAny<string>(), It.IsAny<MatchRequest>())).Returns(Task.FromResult(agentTest));

                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                var builder = WebApplication.CreateBuilder();

                IServiceCollection services = new ServiceCollection()
                    .AddSingleton<IConfiguration>(config)
                    .AddScoped(provider => mockHttpService.Object)
                    .AddDataProviderServiceConfig(builder.Configuration)
                    .AddDataProviderServiceGroup(builder.Configuration)
                    .AddScoped<IDataProviderResolverService, DataServiceResolver>();

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