using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Linq;

namespace AdminBoard.Models.FuzzyGreenHouse
{
    public class Value
    {
        public int ValueID { get; set; }
        public string Name { get; set; }
        public List<float> XCoords { get; set; }
        public List<float> YCoords { get; set; }
        public Set Set { get; set; }

        public Value()
        {

        }

        public Value(string valueName, List<float> xs, List<float> ys)
        {
            Name = valueName;
            XCoords = xs;
            YCoords = ys;
        }
    }

    public class ValueConfiguration : IEntityTypeConfiguration<Value>
    {
        public void Configure(EntityTypeBuilder<Value> builder)
        {
            var listConverter = new ValueConverter<List<float>, string>(
                v => string.Join(",", v),
                w => w.Split(',', System.StringSplitOptions.None).Select(q => float.Parse(q)).ToList()
            );

            builder.Property("XCoords").HasConversion(listConverter);
            builder.Property("YCoords").HasConversion(listConverter);
        } 
    }
}
