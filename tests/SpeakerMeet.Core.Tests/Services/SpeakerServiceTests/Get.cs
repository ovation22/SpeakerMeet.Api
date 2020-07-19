using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class Get : SpeakerServiceTestBase
    {
        private readonly Guid _id;
        private readonly Speaker _speaker;

        public Get()
        {
            _id = new Guid("1BF95F24-0C0B-4C9A-8B80-46110CA9413E");
            _speaker = new Speaker
                {
                    Id = _id
                };

            Repository.Setup(x => x.Get(It.IsAny<Expression<Func<Speaker, bool>>>())).ReturnsAsync(_speaker);
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
    }
}