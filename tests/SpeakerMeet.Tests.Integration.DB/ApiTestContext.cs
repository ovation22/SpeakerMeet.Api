using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace SpeakerMeet.Tests.Integration.DB
{
    public class ApiTestContext
    {
        public HttpClient HttpClient { get; set; }

        public ApiTestContext()
        {
            IConfiguration configuration = BuildConfiguration();

            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration["API_URL"])
            };
        }

        private IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("settings.local.json", true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
