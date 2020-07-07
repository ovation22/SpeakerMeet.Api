using SpeakerMeet.Api.Controllers;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.CommunitiesControllerTests
{
    public class GetAll
    {
        [Fact]
        public void ItReturns()
        {
            // Arrange
            var controller = new CommunitiesController();

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsAssignableFrom<string[]>(result);
        }
    }
}
