using System;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Exceptions;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.ConferenceServiceTests
{
    public class Get : ConferenceServiceTestBase
    {
        private readonly Guid _id;

        public Get()
        {
            _id = new Guid("B703FC6F-B72D-4048-9FEA-5264A50F8363");
            var conference = new Conference
            {
                Id = _id
            };

            Repository.Setup(x => x.Get(It.IsAny<ConferenceSpecification>())).ReturnsAsync(conference);
        }

        [Fact]
        public async Task ItReturnsConference()
        {
            // Arrange
            // Act
            var conference = await Service.Get(_id);

            // Assert
            Assert.NotNull(conference);
            Assert.IsAssignableFrom<ConferenceResult>(conference);
        }

        [Fact]
        public async Task WhenConferenceNull_ThenEntityNotFoundException()
        {
            // Arrange
            Repository.Setup(x => x.Get(It.IsAny<ConferenceSpecification>())).ReturnsAsync((Conference?)null);

            // Act
            Task Act() => Service.Get(_id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(Act);
        }
    }
}