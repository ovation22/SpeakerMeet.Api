using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "SpeakerMeetRepository")]
    public class GetAll : EFRepositoryTestBase
    {
        public GetAll(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItReturnsAllSpeaker()
        {
            // Arrange
            // Act
            var speakers = (List<Speaker>)await Repository.GetAll<Speaker>();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Speaker>>(speakers);
            Assert.Equal(Context.Speakers.Count(), speakers.Count);
        }
    }
}