using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LAF
{
    namespace ServiceUnitTest
    {
        public class APITests
        {
            [Fact]
            public void Test1()
            {
                public async Task APIReturnsDataAsync()
                {
                    // Create mock for service dependency
                    var mockService = new Mock<IABSDataService>();

                    //This is how to mock an asynchronous method
                    var myType = mockService.Setup(p => p.GetDataByRegionIdAndSexIdAsync(104, 1)).Returns(Task.FromResult(new PopulationData { RegionCode = 104, RegionName = "West Coast" }));

                    //Testing /api/age-structure/104/1 endpoint.
                    var controller = new AbsDataController(mockService.Object);

                    //Test that api call returns a successful ActionResult
                    var model = await controller.GetData(104, 1) as OkObjectResult;

                    Assert.NotNull(model);

                    //Test that object embeded in ActionResult is correct type.
                    var result = model.Value as PopulationData;

                    Assert.NotNull(result);
                }
            }
        }
    }
}