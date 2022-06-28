using FuzzyLib;
using System.Collections.Generic;

namespace GreenhouseCore
{
    public class FGCData
    {
        public FGCData() { }
        public FGCData(List<FuzzyInputSet> inputSets, List<FuzzyOutputSet> outputSets, List<FuzzyRules> rules)
        {
            FuzzyInputSets = inputSets;
            FuzzyOutputSets = outputSets;
            FuzzyRules = rules;
        }

        public List<FuzzyInputSet> FuzzyInputSets { get; set; }
        public List<FuzzyOutputSet> FuzzyOutputSets { get; set; }
        public List<FuzzyRules> FuzzyRules { get; set; }
    }
}
