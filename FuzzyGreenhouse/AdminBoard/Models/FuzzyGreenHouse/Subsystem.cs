using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AdminBoard.Models.FuzzyGreenHouse
{
    public class Subsystem
    {
        public int SubsystemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Subsystem() { }

        public Subsystem(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    public class SubsystemConfiguration : IEntityTypeConfiguration<Version>
    {
        public void Configure(EntityTypeBuilder<Version> builder)
        {
        }
    }
}
