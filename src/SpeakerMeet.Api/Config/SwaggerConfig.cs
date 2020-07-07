using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SpeakerMeet.Api.Config
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SpeakerMeet",
                    Description = "A RESTful API for SpeakerMeet",
                });
            });
        }
        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpeakerMeet API V1");
            });
        }
    }
}