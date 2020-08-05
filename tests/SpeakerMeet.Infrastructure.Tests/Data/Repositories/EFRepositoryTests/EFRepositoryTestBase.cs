using System;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;
using Xunit;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    public class EFRepositoryTestBase : IClassFixture<ContextFixture>
    {
        protected readonly SpeakerMeetContext Context;
        protected readonly SpeakerMeetRepository Repository;

        public EFRepositoryTestBase(ContextFixture fixture)
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
