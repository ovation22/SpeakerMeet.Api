using System.Threading.Tasks;
using Moq;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Caching.CacheManagerTests
{
    [Trait("Category", "CacheManager")]
    public class Remove : CacheManagerTestBase
    {
        private readonly string _key;

        public Remove()
        {
            _key = "key";
        }

        [Fact]
        public async Task ItCallsRemove()
        {
            // Arrange
            // Act 
            await CacheManager.Remove(_key);

            // Assert
            CacheAdapter.Verify(x => x.RemoveAsync(_key), Times.Once);
        }
    }
}
