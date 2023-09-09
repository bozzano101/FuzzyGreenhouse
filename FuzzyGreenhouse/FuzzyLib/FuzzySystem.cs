using System.Collections.Generic;

namespace FuzzyLib
{
    /// <summary>
    /// DTO for returning calculated outputs
    /// </summary>
    public record CalcultedOutput
    {
        /// <summary>
        /// Id of output set for which we have calculated output value
        /// </summary>
        public int OutputSetId { get; set; }
        /// <summary>
        /// Value that is calculated
        /// </summary>
        public float Value { get; set; }
    }

    /// <summary>
    /// This class represents major class whitin all operations on fuzzy system should be perfomed.
    /// </summary>
    public class FuzzySystem
    {
        /// <summary>
        /// Field that represents all input variables (sets) that will affect output result
        /// </summary>
        public List<FuzzyInputSet> InputSets { get; set; } = new List<FuzzyInputSet>();
        /// <summary>
        /// Field that represent all output sets
        /// </summary>
        public List<FuzzyOutputSet> OutputSets { get; set; } = new List<FuzzyOutputSet>();
        /// <summary>
        /// Field that represent all rules for determining output value
        /// </summary>
        public List<FuzzyRules> Rules { get; set; } = new List<FuzzyRules>();
        /// <summary>
        /// Field that represents Fuzzy system name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Field that represents Fuzzy system description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Field that represents Fuzzy system Id
        /// </summary>
        public int Id { get; set; }

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
        public List<CalcultedOutput> CalculateOutputs()
        {
            var result = new List<CalcultedOutput>();

            foreach(var output in OutputSets)
            {
                UpdateOutputValue();

                float up = 0, down = 0;
                foreach (var outputValue in output.Values)
                {
                    up += outputValue.Value * outputValue.WeightedAverage;
                    down += outputValue.Value;
                }

                ResetOutputValue();
                result.Add(new CalcultedOutput { OutputSetId = output.Id, Value = (up / down) });
            }

            return result;
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
