using SpeakerMeet.Core.Interfaces.Repositories;

namespace SpeakerMeet.Infrastructure.Data.Repositories
{
    public class SpeakerMeetRepository : EFRepository, ISpeakerMeetRepository
    {
        private readonly SpeakerMeetContext _context;

        public SpeakerMeetRepository(SpeakerMeetContext context) : base(context)
        {
            _context = context;
        }
    }
}
