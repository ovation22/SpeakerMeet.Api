using System;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SpeakerMeet.Api.Config;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;
using SpeakerMeet.Core.Interfaces.Utilities;
using SpeakerMeet.Core.Services;
using SpeakerMeet.Infrastructure.Caching;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using SpeakerMeet.Infrastructure.Logging;
using SpeakerMeet.Infrastructure.Utilities;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
    lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCorsConfig();
builder.Services.AddControllersConfig();
builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddSwaggerConfig();

builder.Services.AddHealthCheckConfig(builder.Configuration);

builder.Services.AddSingleton<ITimeManager, TimeManager>();
builder.Services.AddSingleton<ICacheManager, CacheManager>();
builder.Services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
builder.Services.AddScoped(typeof(ISpeakerMeetRepository), typeof(SpeakerMeetRepository));

builder.Services.AddCaching(builder.Configuration, builder.Environment.IsDevelopment());

builder.Services.AddScoped<ISpeakerService, SpeakerService>();
builder.Services.AddScoped<ICommunityService, CommunityService>();
builder.Services.AddScoped<IConferenceService, ConferenceService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<ISpeakerPresentationService, SpeakerPresentationService>();

builder.Services.AddSingleton<ISearchService>(
    new SearchService(builder.Configuration["SearchServiceName"], builder.Configuration["SearchServiceQueryApiKey"],
        builder.Configuration["SearchIndexName"]));

builder.Services.AddApplicationInsightsTelemetry(opt => opt.EnableActiveTelemetryConfigurationSetup = true);
builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
{
    module.EnableSqlCommandTextInstrumentation = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

EnsureDb(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfig();
    app.UseDeveloperExceptionPage();
}
if (!app.Environment.IsDevelopment())
{
    app.UseHealthCheckConfig();
}

app.UseCorsConfig();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void EnsureDb(IServiceProvider services)
{
    using SpeakerMeetContext db = services.CreateScope().ServiceProvider.GetRequiredService<SpeakerMeetContext>();
    db.Database.EnsureCreated();
}

public partial class Program
{
}