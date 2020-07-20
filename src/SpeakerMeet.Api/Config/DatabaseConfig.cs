using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpeakerMeet.Infrastructure.Data;

namespace SpeakerMeet.Api.Config
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<SpeakerMeetContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SpeakerMeet")));
                //options.UseSqlServer(configuration.GetConnectionString("Azure")));
        }
    }
}