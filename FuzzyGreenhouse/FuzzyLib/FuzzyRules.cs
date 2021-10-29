using System;

namespace FuzzyLib
{
    public enum LogicOperator
    {
        AND = 1,
        OR = 1
    }

    public class FuzzyRules
    {
        public FuzzyRules(FuzzyInput input1, FuzzyInput input2, FuzzyOutput output, LogicOperator _operator)
        {
            Input1 = input1;
            Input2 = input2;
            Output = output;
            if (_operator == LogicOperator.AND)
                Output.Mu = Math.Max(Output.Mu, Math.Min(input1.Mu, input2.Mu));
            else
                Output.Mu = Math.Max(Output.Mu, Math.Max(input1.Mu, input2.Mu));
        }

        public FuzzyInput Input1 { get; set; }
        public FuzzyInput Input2 { get; set; }
        public FuzzyOutput Output { get; set; }
        public LogicOperator Operator { get; set; }
    }
}
