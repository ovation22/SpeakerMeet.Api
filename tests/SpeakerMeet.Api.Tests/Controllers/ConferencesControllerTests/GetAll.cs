using SpeakerMeet.Api.Controllers;
using Xunit;

namespace SpeakerMeet.Api.Tests.Controllers.ConferencesControllerTests
{
    public class GetAll
    {
        [Fact]
        public void ItReturns()
        {
            // Arrange
            var controller = new ConferencesController();

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsAssignableFrom<string[]>(result);
        }
    }
}
