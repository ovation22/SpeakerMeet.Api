using SpeakerMeet.Core.Interfaces.Repositories;

namespace SpeakerMeet.Infrastructure.Data.Repositories
{
    public class SpeakerMeetRepository : EFRepository, ISpeakerMeetRepository
    {
        public SpeakerMeetRepository(SpeakerMeetContext context) : base(context)
        {
        }
    }
}
