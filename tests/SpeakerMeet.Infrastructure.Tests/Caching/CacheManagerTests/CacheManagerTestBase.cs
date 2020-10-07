using Microsoft.Extensions.Options;
using Moq;
using SpeakerMeet.Core.Cache;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Infrastructure.Caching;

namespace SpeakerMeet.Infrastructure.Tests.Caching.CacheManagerTests
{
    public class CacheManagerTestBase
    {
        protected readonly CacheManager CacheManager;
        protected readonly Mock<IOptions<CacheConfig>> CacheOptions;
        protected internal Mock<IDistributedCacheAdapter> CacheAdapter;

        public CacheManagerTestBase()
        {
            CacheOptions = new Mock<IOptions<CacheConfig>>();
            CacheAdapter = new Mock<IDistributedCacheAdapter>();

            CacheOptions.Setup(x => x.Value).Returns(() => new CacheConfig { DefaultExpirationMinutes = 2 });

            CacheManager = new CacheManager(CacheAdapter.Object, CacheOptions.Object);
        }
    }
}
