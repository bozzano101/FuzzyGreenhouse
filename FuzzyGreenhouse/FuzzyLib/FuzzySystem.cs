using System.Collections.Generic;

namespace FuzzyLib
{
    /// <summary>
    /// This class represents major class whitin all operations on fuzzy system should be perfomed.
    /// </summary>
    public class FuzzySystem
    {
        /// <summary>
        /// Field that represents all input variables (sets) that will affect output result
        /// </summary>
        public List<FuzzyInputSet> InputSets { get; set; }
        /// <summary>
        /// Field that represent output result based upon input variables
        /// </summary>
        public FuzzyOutputSet OutputSet { get; set; }
        /// <summary>
        /// Field that represent all rules for determining output value
        /// </summary>
        public List<FuzzyRules> Rules { get; set; }

        /// <summary>
        /// Constructor that accept sets and rules from other services
        /// </summary>
        /// <param name="inputSets"> Input variables (sets) </param>
        /// <param name="outputSet"> Output variables (sets) </param>
        /// <param name="rules"> Rules </param>
        public FuzzySystem(List<FuzzyInputSet> inputSets, FuzzyOutputSet outputSet, List<FuzzyRules> rules)
        {
            InputSets = inputSets;
            Rules = rules;
            OutputSet = outputSet;
        }

        /// <summary>
        /// Method used for changing value in one of Input sets.
        /// </summary>
        /// <param name="value"> Value to change set input to. Mandatory field</param>
        /// <param name="id"> If want to search for set by ID, provide this field </param>
        /// <param name="name"> If want to search for set by Name, provide this field</param>
        public void ChangeInputSetValue(float value, int? id, string name = null)
        {
            InputSets.ForEach(set =>
            {
                if ((id.HasValue && set.Id == id) || (name != null && set.Name == name))
                {
                    set.RecalculateFunctionsValues(value);
                    return;
                }
            });
        }

        /// <summary>
        /// This method is used to generate output value for currently setted input values. Returns output value
        /// </summary>
        /// <returns></returns>
        public float CalculateOutput()
        {
            UpdateOutputValue();

            float up = 0, down = 0;
            foreach(var outputValue in OutputSet.Values)
            {
                up += outputValue.Value * outputValue.WeightedAverage;
                down += outputValue.Value;
            }

            ResetOutputValue();
            return (up / down);
        }

        /// <summary>
        /// This method is used for recalculating outputs (indicated by all rules)
        /// </summary>
        private void UpdateOutputValue()
        {
            Rules.ForEach(rule => {
                rule.RecalculateOutputValue();
            });
        }

        /// <summary>
        /// This method is used for reseting output for each rule before new fuzzy calculation is made
        /// </summary>
        private void ResetOutputValue()
        {
            Rules.ForEach(rule => {
                rule.ResetOutputValue();
            });
        }
    }
}
