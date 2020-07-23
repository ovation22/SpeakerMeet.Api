using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Core.Tests.Services.SpeakerServiceTests
{
    public class GetBySlug : SpeakerServiceTestBase
    {
        private readonly string _slug;
        private readonly Speaker _speaker;

        public GetBySlug()
        {
            _slug = "test-slug";
            _speaker = new Speaker
                {
                    Slug = _slug
                };

            Repository.Setup(x => x.Get(It.IsAny<Expression<Func<Speaker, bool>>>())).ReturnsAsync(_speaker);
        }

        [Fact]
        public async Task ItReturnsSpeaker()
        {
            // Arrange
            // Act
            var speaker = await Service.Get(_slug);

            // Assert
            Assert.NotNull(speaker);
            Assert.IsAssignableFrom<SpeakerResult>(speaker);
        }
    }
}