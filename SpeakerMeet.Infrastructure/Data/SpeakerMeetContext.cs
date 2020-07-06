using Microsoft.EntityFrameworkCore;

namespace SpeakerMeet.Infrastructure.Data
{
    public class SpeakerMeetContext : DbContext
    {
        public SpeakerMeetContext(DbContextOptions<SpeakerMeetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}