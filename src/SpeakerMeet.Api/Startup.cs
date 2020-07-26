using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpeakerMeet.Api.Config;
using SpeakerMeet.Core.Cache;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;
using SpeakerMeet.Core.Interfaces.Utilities;
using SpeakerMeet.Core.Services;
using SpeakerMeet.Infrastructure.Caching;
using SpeakerMeet.Infrastructure.Data.Repositories;
using SpeakerMeet.Infrastructure.Logging;
using SpeakerMeet.Infrastructure.Utilities;

namespace SpeakerMeet.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _hostContext;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostContext)
        {
            Configuration = configuration;
            _hostContext = hostContext;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsConfig();
            services.AddControllersConfig();
            services.AddDatabaseConfig(Configuration);
            services.AddSwaggerConfig();
            services.AddApplicationInsightsTelemetry();

            services.AddSingleton<ITimeManager, TimeManager>();
            services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            services.AddScoped(typeof(ISpeakerMeetRepository), typeof(SpeakerMeetRepository));

            services.AddDistributedMemoryCache();
            services.Configure<CacheConfig>(Configuration.GetSection("Cache"));
            services.AddSingleton(typeof(IDistributedCacheAdapter), typeof(DistributedCacheAdapter));

            services.AddScoped<ISpeakerService, SpeakerService>();
            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<IConferenceService, ConferenceService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            services.AddSingleton<ISearchService>(
                new SearchService(Configuration["SearchServiceName"], Configuration["SearchServiceQueryApiKey"], Configuration["SearchIndexName"]));

            if (_hostContext.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Configuration["Cache:Configuration"];
                    options.InstanceName = Configuration["Cache:InstanceName"];
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerConfig();
            app.UseCorsConfig();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
