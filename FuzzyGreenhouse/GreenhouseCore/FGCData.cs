using FuzzyLib;
using System.Collections.Generic;
using System.Linq;

namespace GreenhouseCore
{
    public class FGCData
    {
        private List<FuzzyInputSet> fuzzyInputSets;
        private List<FuzzyOutputSet> fuzzyOutputSets;
        private List<FuzzyRules> fuzzyRules;

        public List<FuzzySystem> FuzzySystems { get; set; }

        public FGCData() { }
        public FGCData(List<FuzzyInputSet> inputSets, List<FuzzyOutputSet> outputSets, List<FuzzyRules> rules)
        {
            fuzzyInputSets = inputSets;
            fuzzyOutputSets = outputSets;
            fuzzyRules = rules;

            FuzzySystems = new List<FuzzySystem>();

            PrepareFuzzySystems();
        }

        private void PrepareFuzzySystems()
        {
            foreach (var outputSet in fuzzyOutputSets)
            {
                var rulesForOutputSet = fuzzyRules.Where(e => 
                {
                    var outputRuleName = DatabaseBridge.GetOutputSetName(e.Output.Id);
                    return outputSet.Name.Equals(outputRuleName);
                }).ToList();

                var fuzzySystem = new FuzzySystem(
                    fuzzyInputSets,
                    new List<FuzzyOutputSet> { outputSet },
                    rulesForOutputSet
                );

                FuzzySystems.Add(fuzzySystem);
            }
        }


    }
}
