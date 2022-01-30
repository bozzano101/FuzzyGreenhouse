using System;
using System.Diagnostics;

namespace FuzzyLib
{
    /// <summary>
    /// Enum describes logical AND and OR operators
    /// </summary>
    public enum LogicOperator
    {
        /// <summary>
        /// AND operator
        /// </summary>
        AND = 1,
        /// <summary>
        /// OR operator
        /// </summary>
        OR = 1
    }

    /// <summary>
    /// This class represents fuzzy rules that are used for deciding output values. It gets two inputs and based on logical operator
    /// calculates Mu value for output set. After this calculation, it can be used for fuzzy deciding.
    /// </summary>
    public class FuzzyRules
    {
        /// <summary>
        /// Constructor function for FuzzyRules class
        /// </summary>
        /// <param name="input1">First input set functon</param>
        /// <param name="input2">Second input set function</param>
        /// <param name="output">Output set function</param>
        /// <param name="_operator">Logical operator for deciding</param>
        public FuzzyRules(FuzzyInput input1, FuzzyInput input2, FuzzyOutput output, LogicOperator _operator)
        {
            Input1 = input1;
            Input2 = input2;
            Output = output;
            Operator = _operator;
            RecalculateOutputMu();
        }

        public void RecalculateOutputMu()
        {
            if (Operator == LogicOperator.AND)
                Output.Mu = Math.Max(Output.Mu, Math.Min(Input1.Mu, Input2.Mu));
            else
                Output.Mu = Math.Max(Output.Mu, Math.Max(Input1.Mu, Input2.Mu));
            Console.WriteLine(Output.Mu);
        }

        /// <summary>
        /// Input1 - First input set functon
        /// </summary>
        public FuzzyInput Input1 { get; set; }

        /// <summary>
        /// Input2 - Second input set functon
        /// </summary>
        public FuzzyInput Input2 { get; set; }

        /// <summary>
        /// Output - Output set functon
        /// </summary>
        public FuzzyOutput Output { get; set; }

        /// <summary>
        /// Operator - Logical operator for calculating output value
        /// </summary>
        public LogicOperator Operator { get; set; }
    }
}
