using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBoard.Models.FuzzyGreenHouse
{
    public enum SetType
    {
        Input,
        Output
    }

    public class Set
    {
        public int SetID { get; set; }
        public string Name { get; set; }
        public ICollection<Value> Values { get; set; }
        public SetType Type { get; set; }
        public Sensor Sensor { get; set; }

        public Set()
        {

        }

        public Set(string name, SetType type)
        {
            Name = name;
            Type = type;
        }
    }

    public class SetConfiguration : IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> builder)
        {
        }
    }
}
