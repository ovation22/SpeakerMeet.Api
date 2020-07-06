using Microsoft.EntityFrameworkCore;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Infrastructure.Data
{
    public class SpeakerMeetContext : DbContext
    {
        public SpeakerMeetContext(DbContextOptions<SpeakerMeetContext> options) : base(options)
        {
        }

        public virtual DbSet<Speaker> Speakers { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}