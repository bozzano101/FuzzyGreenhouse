using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AdminBoard.Models.FuzzyGreenHouse
{
    public class Version
    {
        public int VersionID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        public Version(string description)
        {
            CreatedDate = DateTime.UtcNow;
            Description = description;
        }
    }

    public class VersionConfiguration : IEntityTypeConfiguration<Version>
    {
        public void Configure(EntityTypeBuilder<Version> builder)
        {
        }
    }
}
