using System;

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
        AND,
        /// <summary>
        /// OR operator
        /// </summary>
        OR
    }

    /// <summary>
    /// This class represents fuzzy rules that are used for deciding output values. It gets two inputs and based on logical operator
    /// calculates Value value for output set. After this calculation, it can be used for fuzzy deciding.
    /// </summary>
    public class FuzzyRules
    {
        /// <summary>
        /// Constructor function for FuzzyRules class
        /// </summary>
        /// <param name="input1">First input set functon</param>
        /// <param name="input2">Second input set function</param>
        /// <param name="output">Output set function</param>
        /// <param name="logicOperator">Logical operator for deciding</param>
        public FuzzyRules(FuzzyInput input1, FuzzyInput input2, FuzzyOutput output, LogicOperator logicOperator)
        {
            Input1 = input1;
            Input2 = input2;
            Output = output;
            Operator = logicOperator;
            RecalculateOutputValue();
        }

        /// <summary>
        /// This function is used for "refreshing" output value when input is changed. 
        /// </summary>
        public void RecalculateOutputValue()
        {
            if(Input2.Name == "Disabled")
            {
                if (Operator == LogicOperator.AND)
                    Output.Value = Math.Max(Output.Value, Input1.Value);
                else
                    Output.Value = Math.Max(Output.Value, Input1.Value);
            }
            else
            {
                if (Operator == LogicOperator.AND)
                    Output.Value = Math.Max(Output.Value, Math.Min(Input1.Value, Input2.Value));
                else
                    Output.Value = Math.Max(Output.Value, Math.Max(Input1.Value, Input2.Value));
            }
        }

        /// <summary>
        /// This function is used for reseting output value between two calculations for fuzzy output.
        /// </summary>
        public void ResetOutputValue()
        {
            Output.Value = 0;
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
