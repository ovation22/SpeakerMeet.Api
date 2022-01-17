using System;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Exceptions;
using SpeakerMeet.Core.Specifications;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class Get : SpeakerServiceTestBase
    {
        private readonly Guid _id;

        public Get()
        {
            _id = new Guid("1BF95F24-0C0B-4C9A-8B80-46110CA9413E");
            var speaker = new Speaker
            {
                Id = _id
            };

            Repository.Setup(x => x.Get(It.IsAny<SpeakerSpecification>())).ReturnsAsync(speaker);
        }

        [Fact]
        public async Task ItReturnsSpeaker()
        {
            // Arrange
            // Act
            var speaker = await Service.Get(_id);

            // Assert
            Assert.NotNull(speaker);
            Assert.IsAssignableFrom<SpeakerResult>(speaker);
        }

        [Fact]
        public async Task WhenSpeakerNull_ThenEntityNotFoundException()
        {
            // Arrange
            Repository.Setup(x => x.Get(It.IsAny<SpeakerSpecification>())).ReturnsAsync((Speaker?)null);

            // Act
            Task Act() => Service.Get(_id);

            // Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(Act);
        }
    }
}