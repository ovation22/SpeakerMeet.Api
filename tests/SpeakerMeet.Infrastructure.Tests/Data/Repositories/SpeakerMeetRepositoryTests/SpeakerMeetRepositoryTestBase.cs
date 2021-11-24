using System;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests;

public class SpeakerMeetRepositoryTestBase : IClassFixture<ContextFixture>
{
    protected readonly SpeakerMeetContext Context;
    protected readonly SpeakerMeetRepository Repository;

    public SpeakerMeetRepositoryTestBase(ContextFixture fixture)
    {
        Context = fixture.Context;
        Context.Speakers.Add(new Speaker
        {
            Id = Guid.NewGuid(),
            Name = "Test Speaker 1",
            Slug = "test-speaker-1",
            Location = "Tampa, FL",
            Description = "Test Speaker 1 from Tampa, FL"
        });
        Context.Speakers.Add(new Speaker
        {
            Id = Guid.NewGuid(),
            Name = "Test Speaker 2",
            Slug = "test-speaker-2",
            Location = "Tampa, FL",
            Description = "Test Speaker 2 from Tampa, FL"
        });
        Context.Speakers.Add(new Speaker
        {
            Id = Guid.NewGuid(),
            Name = "Test Speaker 3",
            Slug = "test-speaker-3",
            Location = "Tampa, FL",
            Description = "Test Speaker 3 from Tampa, FL"
        });
        Context.SaveChanges();

        Repository = new SpeakerMeetRepository(Context);
    }
}