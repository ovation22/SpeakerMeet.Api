using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class GetAll : SpeakerServiceTestBase
    {
        [Fact]
        public async Task ItReturnsSpeakers()
        {
            // Arrange
            // Act
            var speakers = await Service.GetAll(0, 1);

            // Assert
            Assert.NotNull(speakers);
            Assert.IsAssignableFrom<SpeakersResult>(speakers);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll(0, 1);

            // Assert
            Repository.Verify(x => x.List(It.IsAny<SpeakerSpecification>()), Times.Once());
        }
    }
}
