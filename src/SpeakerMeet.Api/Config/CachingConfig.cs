using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpeakerMeet.Core.Cache;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Infrastructure.Caching;

namespace SpeakerMeet.Api.Config
{
    [ExcludeFromCodeCoverage]
    public static class CachingConfig
    {
        public static void AddCaching(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment hostContext
        )
        {
            services.AddDistributedMemoryCache();
            services.Configure<CacheConfig>(configuration.GetSection("Cache"));
            services.AddSingleton(typeof(IDistributedCacheAdapter), typeof(DistributedCacheAdapter));

            if (hostContext.IsDevelopment() || hostContext.IsEnvironment("Local"))
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration["Cache:Configuration"];
                    options.InstanceName = configuration["Cache:InstanceName"];
                });
            }
        }
    }
}