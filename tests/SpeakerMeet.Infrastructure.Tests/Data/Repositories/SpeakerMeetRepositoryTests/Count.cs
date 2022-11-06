using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.Core.Models.Entities;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "SpeakerMeetRepository")]
    public class Count : SpeakerMeetRepositoryTestBase
    {
        public Count(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItReturnsSpeaker()
        {
            // Arrange
            // Act
            var count = await Repository.Count<Speaker>();

            // Assert
            Assert.IsAssignableFrom<int>(count);
            Assert.Equal(Context.Speakers.Count(), count);
        }
    }
}