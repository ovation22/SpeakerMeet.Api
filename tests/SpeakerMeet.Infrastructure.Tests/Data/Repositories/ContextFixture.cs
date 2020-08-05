using System;
using Microsoft.EntityFrameworkCore;
using SpeakerMeet.Infrastructure.Data;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories
{
    public class ContextFixture : IDisposable
    {
        public SpeakerMeetContext Context { get; }

        public ContextFixture()
        {
            var options = new DbContextOptionsBuilder<SpeakerMeetContext>()
                .UseInMemoryDatabase("SpeakerMeet")
                .Options;

            Context = new SpeakerMeetContext(options);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
