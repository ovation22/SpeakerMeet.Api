using SpeakerMeet.Core.Services;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class Get
    {
        [Fact]
        public void ItExists()
        {
            // Arrange
            // Act
            var service = new SpeakerService();

            // Assert
            Assert.NotNull(service);
        }
    }
}