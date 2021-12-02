using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBoard.Models.FuzzyGreenHouse
{
    public class Sensor
    {
        public int SensorID { get; set; }
        public string Name { get; set; }
        public ICollection<Rule> Rules { get; set; }
        public ICollection<Set> Sets { get; set; }
    }

    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
        }
    }
}
