using System.Collections.Generic;
using System.Threading.Tasks;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Specifications;
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
        public async Task ItReturnsAllSpeaker()
        {
            // Arrange
            var spec = new SpeakerSpecification(1, 1);

            // Act
            var speakers = (List<Speaker>)await Repository.List(spec);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Speaker>>(speakers);
        }
    }
}