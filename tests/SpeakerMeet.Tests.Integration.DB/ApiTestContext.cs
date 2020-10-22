using System;
using System.Net.Http;

namespace SpeakerMeet.Tests.Integration.DB
{
    public class ApiTestContext
    {
        public HttpClient HttpClient { get; set; }

        public ApiTestContext()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(Environment.GetEnvironmentVariable("API_URL")!)
            };
        }
    }
}
