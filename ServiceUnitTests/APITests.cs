using LAF.Models.BusinessObjects;
using LAF.Models.Config;
using LAF.Models.Interfaces.Services;
using LAF.MVC.Controllers;
using LAF.Services.DataProviders;
using LAF.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
                string jsonConfig=@"{
                        ""ServiceConfiguration"": {
                            ""MatchMinimumValue"": 5,
                            ""DataServiceProviders"": [
                            {
                                ""DataProvider"": {
                                     ""ServiceType"": ""MySQLRESTDataProvider"",
                                     ""ServiceUrl"": ""127.0.0.1:5148"",
                                     ""Default"": true
                                }
                            },
                            {
                                ""DataProvider"": {
                                ""ServiceType"": ""MySQLgRPCDataProvider"",
                                ""ServiceUrl"": ""127.0.0.1:3765"",
                                ""Default"": false
                                }
                            },
                            {
                                ""DataProvider"": {
                                ""ServiceType"": ""MongoDbRESTDataProvider"",
                                ""ServiceUrl"": ""127.0.0.1:5436"",
                                ""Default"": false
                            }
                        }
                    ]
                }
            }";

                /*
                TODO: Could use real service provider with resolver service rather than mock.
                But would need to mock Config service since the resolver uses it.

                MySQLRESTDataProvider

                Mocking service provider - 
                IServiceCollection services = new ServiceCollection()
                    .AddSingleton(rootConfig)
                    .AddScoped<IHttpRESTProvider, mockHttpService.Object>()
                    .AddScoped<IDataProviderResolverService, MySQLRESTDataProvider>();

                services.Add(new ServiceDescriptor(typeof(IAgentDataProvider), provider, ServiceLifetime.Scoped));

                var sp = services.BuildServiceProvider();
                */

                var agentTest = new Agent { LicenseNo = "1234", Name = "Dorian Gray" };
                var mockHttpService = new Mock<IHttpRESTProvider>();
                var mockResolverService = new Mock<IDataProviderResolverService>();
                var dataOptions = new MySQLRESTDataProviderOptions(new DataProviderOptions() { Default = true, ServiceType = "MySQLRESTDataProvider", ServiceUrl = "127.0.0.1" }); 
                var matchRequest = new MatchRequest { SizePreference = 6, AttributePreference = "localknowledge" };
                string mockUrl = "127.0.0.1";

                //This is how to mock an asynchronous method.
                var httpType = mockHttpService.Setup(p => p.MatchAgentAsync(mockUrl, matchRequest)).Returns(Task.FromResult(agentTest));
                var agentResolveType = mockResolverService.Setup(p => p.GetDataProvider()).Returns(new MySQLRESTDataProvider(mockHttpService.Object, dataOptions));

                //Testing AgentController/MatchAgent/5 endpoint.
                var controller = new AgentController(mockResolverService.Object);

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