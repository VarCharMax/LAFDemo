using LAF.Models.BusinessObjects;
using LAF.Models.Config;
using LAF.Models.Interfaces.Services;
using LAF.MVC.Controllers;
using LAF.Services.DataProviders;
using LAF.Services.DataProviders.Interfaces;
using LAF.Services.Interfaces;
using LAF.Services.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                var mockHttpService = new Mock<IHttpRESTProvider>();
                var mockResolverService = new Mock<IDataProviderResolverService>();
                var dataOptions = new MySQLRESTDataProviderOptions(new DataProviderOptions() { Default = true, ServiceType = "MySQLRESTDataProvider", ServiceUrl = "127.0.0.1" }); 
                var matchRequest = new MatchRequest { SizePreference = 6, AttributePreference = "localknowledge" };
                string mockUrl = "127.0.0.1";

                //This is how to mock an asynchronous method.
                var httpType = mockHttpService.Setup(p => p.MatchAgentAsync(mockUrl, matchRequest)).Returns(Task.FromResult(new Agent { LicenseNo = "1234", Name = "Dorian Gray" }));
                var agentResolveType = mockResolverService.Setup(p => p.GetDataProvider()).Returns(new MySQLRESTDataProvider(mockHttpService.Object, dataOptions));

                /*
                IServiceCollection services = new ServiceCollection()
                    // .AddSingleton(rootConfig)
                    .AddScoped<IHttpRESTProvider, HttpRESTProvider>()
                    .AddScoped<IAgentDataProvider, MySQLRESTDataProvider>();

                var sp = services.BuildServiceProvider();
                */

                //Testing /api/agent/1 endpoint.
                var controller = new AgentController(mockResolverService.Object);

                //Test that api call returns a successful ActionResult
                var model = await controller.MatchAgentAsync(matchRequest) as OkObjectResult;

                Assert.NotNull(model);

                //Test that object embeded in ActionResult is correct type.
                var result = model.Value as Agent;

                Assert.NotNull(result);
                

                /*
                Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    { "Plugins:PluginA:Test","A"  }
                };
                var config = new ConfigurationBuilder()
                    .AddInMemoryCollection(dic)
                    .Build();

                PluginFactoryConfigration rootConfig = new PluginFactoryConfigration(config);

                IServiceCollection services = new ServiceCollection()
                    .AddSingleton(rootConfig)
                    .AddSingleton(typeof(IPluginConfigrationProvider<>), typeof(PluginConfigrationProvider<>))
                    .AddOptions()
                    .ConfigureOptions<PluginConfigrationOptions<PluginA, PluginAOptions>>();

                var sp = services.BuildServiceProvider();
                var options = sp.GetRequiredService<IOptions<PluginAOptions>>();

                Assert.Equal("A", options.Value.Test);


                */
            }
        }
    }
}