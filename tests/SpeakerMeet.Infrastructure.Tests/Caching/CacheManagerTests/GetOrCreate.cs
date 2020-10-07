using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using SpeakerMeet.Core.DTOs;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Caching.CacheManagerTests
{
    [Trait("Category", "CacheManager")]
    public class GetOrCreate : CacheManagerTestBase
    {
        private bool _methodCalled;
        private readonly string _key;
        private readonly List<CommunityFeatured>? _results;

        public GetOrCreate()
        {
            _key = "key";
            _methodCalled = false;
            _results = new List<CommunityFeatured>
            {
                new CommunityFeatured
                {
                    Name = "Name",
                    Description = "Description"
                }
            };
        }

        [Fact]
        public async Task Given_WhenCacheHit_CacheValueReturned()
        {
            // Arrange
            CacheAdapter.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(JsonSerializer.Serialize(_results));

            // Act 
            var result = await CacheManager.GetOrCreate(_key, async () => await DefaultMethod());

            // Assert
            Assert.False(_methodCalled);
            Assert.IsAssignableFrom<IEnumerable<CommunityFeatured>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Given_WhenCacheMiss_ExecutesFunction()
        {
            // Arrange
            CacheAdapter.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(string.Empty);

            // Act 
            var result = await CacheManager.GetOrCreate(_key, async () => await DefaultMethod());

            // Assert
            Assert.True(_methodCalled);
            Assert.IsAssignableFrom<IEnumerable<CommunityFeatured>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Given_WhenCacheMiss_SetsCache()
        {
            // Arrange
            CacheAdapter.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(string.Empty);

            // Act 
            await CacheManager.GetOrCreate(_key, async () => await DefaultMethod());

            // Assert
            var value = JsonSerializer.Serialize(_results!.ToList());
            CacheAdapter.Verify(x => x.SetStringAsync(_key, value, It.IsAny<DistributedCacheEntryOptions>()));
        }

        private async Task<IEnumerable<CommunityFeatured>> DefaultMethod()
        {
            _methodCalled = true;

            return await Task.FromResult(_results.AsEnumerable());
        }
    }
}
