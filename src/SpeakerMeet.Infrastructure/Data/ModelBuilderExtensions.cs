using System;
using Microsoft.EntityFrameworkCore;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Community>().HasData(
                new Community { Id = new Guid("725C6768-7EAB-43B0-AA39-86F15E97824A"), Name = "Sample Community", Slug = "sample-community", Location = "Tampa, FL", Description = "Sample Community", IsActive = true, Created = DateTime.UtcNow, Updated = DateTime.UtcNow}
            );

            modelBuilder.Entity<Conference>().HasData(
                new Community { Id = new Guid("BE0CBB60-FF0B-4E47-8E37-8F024AF1A5D2"), Name = "Sample Conference", Slug = "sample-conference", Location = "New York, NY", Description = "Sample Conference", IsActive = true, Created = DateTime.UtcNow, Updated = DateTime.UtcNow }
            );

            modelBuilder.Entity<Speaker>().HasData(
                new Speaker { Id = new Guid("AA21D845-0562-4714-8DE0-04B4971C702B"), Name = "Sample Speaker", Slug = "sample-speaker", Location = "Louisville, KY", Description = "Sample Speaker", IsActive = true, Created = DateTime.UtcNow, Updated = DateTime.UtcNow }
            );
        }
    }
}
