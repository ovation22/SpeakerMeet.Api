using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.SpeakerPresentationServiceTests
{
    public class GetAll : SpeakerPresentationServiceTestBase
    {
        private readonly Guid _id;

        public GetAll()
        {
            _id = new Guid("445849F4-36CD-4A97-B5EA-E46328FD93A7");
        }

        [Fact]
        public async Task ItReturnsSpeakers()
        {
            // Arrange
            // Act
            var speakers = await Service.GetAll(_id);

            // Assert
            Assert.NotNull(speakers);
            Assert.IsAssignableFrom<IEnumerable<SpeakerPresentationsResult>>(speakers);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll(_id);

            // Assert
            Repository.Verify(x => x.List(It.IsAny<SpeakerPresentationSpecification>()), Times.Once());
        }
    }
}
