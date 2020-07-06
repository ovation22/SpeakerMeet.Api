using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SpeakerMeet.Api.Config
{
    [ExcludeFromCodeCoverage]
    public static class CorsConfig
    {
        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAll",
                p => p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
        }

        public static void UseCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors("AllowAll");
        }
    }
}