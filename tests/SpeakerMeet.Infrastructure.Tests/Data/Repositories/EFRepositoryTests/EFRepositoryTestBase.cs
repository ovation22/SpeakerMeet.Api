using Microsoft.EntityFrameworkCore;
using SpeakerMeet.Infrastructure.Data;

namespace SpeakerMeet.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    public class EFRepositoryTestBase
    {
        protected DbContextOptions<SpeakerMeetContext> Options;

        public EFRepositoryTestBase()
        {
            Options = new DbContextOptionsBuilder<SpeakerMeetContext>()
                .UseInMemoryDatabase("SpeakerMeet")
                .Options;
        }
    }
}
