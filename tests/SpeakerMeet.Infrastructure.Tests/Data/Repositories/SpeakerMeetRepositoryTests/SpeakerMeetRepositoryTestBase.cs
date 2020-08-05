using System;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    public class SpeakerMeetRepositoryTestBase : IClassFixture<ContextFixture>
    {
        protected readonly SpeakerMeetContext Context;
        protected readonly SpeakerMeetRepository Repository;

        public SpeakerMeetRepositoryTestBase(ContextFixture fixture)
        {
            Context = fixture.Context;
            Context.Speakers.Add(new Speaker { Id = Guid.NewGuid() });
            Context.Speakers.Add(new Speaker { Id = Guid.NewGuid() });
            Context.Speakers.Add(new Speaker { Id = Guid.NewGuid() });
            Context.SaveChanges();

            Repository = new SpeakerMeetRepository(Context);
        }
    }
}
