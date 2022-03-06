using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;

namespace SpeakerMeet.Tests.Integration;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            services.AddLogging(c =>
                c.ClearProviders()
            );

            ServiceDescriptor descriptor = services.Single(
                d => d.ServiceType ==
                     typeof(DbContextOptions<SpeakerMeetContext>));

            services.Remove(descriptor);

            services.AddDbContextPool<SpeakerMeetContext>(options =>
            {
                options.UseInMemoryDatabase("Default");
            });

            ServiceProvider? sp = services.BuildServiceProvider();

            using IServiceScope scope = sp.CreateScope();
            IServiceProvider scopedServices = scope.ServiceProvider;
            SpeakerMeetContext db = scopedServices.GetRequiredService<SpeakerMeetContext>();

            ILogger<CustomWebApplicationFactory<TStartup>> logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            db.Database.EnsureCreated();

            try
            {
                InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {Message}", ex.Message);
            }
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