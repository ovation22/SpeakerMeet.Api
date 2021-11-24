using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;

namespace SpeakerMeet.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.Single(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<SpeakerMeetContext>));

                services.Remove(descriptor);

                services.AddDbContextPool<SpeakerMeetContext>(options =>
                {
                    options.UseInMemoryDatabase("Default");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<SpeakerMeetContext>();

                db.Database.EnsureCreated();

                InitializeDbForTests(db);
            });
        }

        private void InitializeDbForTests(SpeakerMeetContext db)
        {
            db.Speakers.RemoveRange(db.Speakers);
            db.Speakers.Add(new Speaker
            {
                Id = Guid.NewGuid(),
                Name = "Test Speaker",
                Slug = "test-speaker",
                Location = "Tampa, FL",
                Description = "Test Speaker from Tampa, FL"
            });
            db.SaveChanges();
        }
    }
}