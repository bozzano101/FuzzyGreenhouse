using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLib
{
    public class FuzzySystem
    {
        public List<FuzzyInputSet> InputSets { get; set; }
        public FuzzyOutputSet OutputSet { get; set; }
        public List<FuzzyRules> Rules { get; set; }

        public FuzzySystem() { }

        public FuzzySystem(List<FuzzyInputSet> inputSets, List<FuzzyOutputSet> outputSet, List<FuzzyRules> rules)
        {
            InputSets = inputSets;
            Rules = rules;
            if (outputSet.Count == 1)
                OutputSet = outputSet[0];
            else
                throw new Exception("Output set list has more than one set.");
        }

        public void ChangeSetMuValue(float value, int? id, string name = null)
        {
            InputSets.ForEach(set =>
            {
                if ((id.HasValue && set.Id == id) || (name != null && set.Name == name))
                {
                    set.RecalculateMu(value);
                    return;
                }
            });
            UpdateOutputMu();
        }

        private void UpdateOutputMu()
        {
            Rules.ForEach(rule => {
                rule.RecalculateOutputMu();
            });
        }

        public float CalculateOutput()
        {
            float up = 0, down = 0;
            foreach(var outputValue in OutputSet.Values)
            {
                up += outputValue.Mu * outputValue.Centroid;
                down += outputValue.Mu;
            }

            return (up / down);
        }


    }
}
