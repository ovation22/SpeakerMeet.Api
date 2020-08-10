using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "SpeakerMeetRepository")]
    public class GetAll : SpeakerMeetRepositoryTestBase
    {
        public GetAll(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItReturnsAllSpeakerByExpression()
        {
            // Arrange
            // Act
            var horses = (List<Speaker>)await Repository.GetAll<Speaker>(x => true);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Speaker>>(horses);
            Assert.Equal(Context.Speakers.Count(), horses.Count);
        }
    }
}