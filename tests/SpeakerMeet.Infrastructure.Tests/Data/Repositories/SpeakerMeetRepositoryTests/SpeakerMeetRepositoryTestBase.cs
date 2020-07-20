using Microsoft.EntityFrameworkCore;
using SpeakerMeet.Infrastructure.Data;
using SpeakerMeet.Infrastructure.Data.Repositories;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.SpeakerMeetRepositoryTests
{
    public class SpeakerMeetRepositoryTestBase
    {
        protected SpeakerMeetRepository Repository = null!;
        protected DbContextOptions<SpeakerMeetContext> Options;

        public SpeakerMeetRepositoryTestBase()
        {
            Options = new DbContextOptionsBuilder<SpeakerMeetContext>()
                .UseInMemoryDatabase("SpeakerMeet")
                .Options;
        }
    }
}