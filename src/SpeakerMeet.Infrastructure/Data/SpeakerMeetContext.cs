using Microsoft.EntityFrameworkCore;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Infrastructure.Data
{
    public class SpeakerMeetContext : DbContext
    {
        public SpeakerMeetContext(DbContextOptions<SpeakerMeetContext> options) : base(options)
        {
        }

        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Search> Searches { get; set; } = null!;
        public virtual DbSet<Speaker> Speakers { get; set; } = null!;
        public virtual DbSet<Community> Communities { get; set; } = null!;
        public virtual DbSet<Conference> Conferences { get; set; } = null!;
        public virtual DbSet<SpeakerTag> SpeakerTags { get; set; } = null!;
        public virtual DbSet<CommunityTag> CommunityTags { get; set; } = null!;
        public virtual DbSet<ConferenceTag> ConferenceTags { get; set; } = null!;
        public virtual DbSet<SocialPlatform> SocialPlatforms { get; set; } = null!;
        public virtual DbSet<SpeakerPresentation> SpeakerPresentations { get; set; } = null!;
        public virtual DbSet<SpeakerSocialPlatform> SpeakerSocialPlatforms { get; set; } = null!;
        public virtual DbSet<CommunitySocialPlatform> CommunitySocialPlatforms { get; set; } = null!;
        public virtual DbSet<ConferenceSocialPlatform> ConferenceSocialPlatforms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            modelBuilder.Entity<Search>(x =>
            {
                x.HasNoKey();
                x.ToView("Search");
            });
        }
    }
}