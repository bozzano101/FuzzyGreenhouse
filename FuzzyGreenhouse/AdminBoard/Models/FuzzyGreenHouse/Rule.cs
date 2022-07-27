using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminBoard.Models.FuzzyGreenHouse
{
    public enum LogicOperator
    {
        AND,
        OR
    }

    public class Rule
    {
        public int RuleID { get; set; }
     
        public Value InputValue1 { get; set; }
        public int InputValue1ID { get; set; }

        public Value InputValue2 { get; set; }
        public int InputValue2ID { get; set; }
        
        public Value OutputValue { get; set; }
        public int OutputValueID { get; set; }

        public Subsystem Subsystem { get; set; }
        public int SubsystemID { get; set; }

        public LogicOperator Operator { get; set; }

        public Rule()
        {

        }

        public Rule(Value inputValue1, Value inputValue2, Value outputValue, LogicOperator logicOperator)
        {
            InputValue1 = inputValue1;
            InputValue2 = inputValue2;
            OutputValue = outputValue;
            Operator = logicOperator;
        }
    }

    public class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
        }
    }
}
