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

                var inputsForThisSystem = new HashSet<FuzzyInputSet>();
                foreach(var rule in rulesForOutputSet)
                {
                    var input1SetName = DatabaseBridge.GetInputSetName(rule.Input1.Id, 1);
                    var input2SetName = DatabaseBridge.GetInputSetName(rule.Input2.Id, 2);

                    inputsForThisSystem.Add(
                        fuzzyInputSets.Where(e => e.Name.Equals(input1SetName)).First()
                    );
                    inputsForThisSystem.Add(
                        fuzzyInputSets.Where(e => e.Name.Equals(input2SetName)).First()
                    );
                }

                var fuzzySystem = new FuzzySystem(
                    inputsForThisSystem.ToList(),
                    new List<FuzzyOutputSet> { outputSet },
                    rulesForOutputSet
                ); ;

                FuzzySystems.Add(fuzzySystem);
            }
        }


    }
}
