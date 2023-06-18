using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AdminBoard.Models.FuzzyGreenHouse
{
    public enum SetType
    {
        Input,
        Output
    }

    public class Set
    {
        public Set() { }

        public Set(string name, SetType type)
        {
            Name = name;
            Type = type;
        }

        public int SetID { get; set; }
        public string Name { get; set; }
        public SetType Type { get; set; }
        public ICollection<Value> Values { get; set; }

        public Subsystem Subsystem { get; set; }
        public int SubsystemID { get; set; }
    }

    public class SetConfiguration : IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> builder)
        {
        }
    }
}
