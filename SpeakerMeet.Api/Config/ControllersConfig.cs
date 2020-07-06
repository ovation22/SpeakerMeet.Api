using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SpeakerMeet.Api.Config
{
    [ExcludeFromCodeCoverage]
    public static class ControllersConfig
    {
        public static void AddControllersConfig(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            });
        }
    }
}