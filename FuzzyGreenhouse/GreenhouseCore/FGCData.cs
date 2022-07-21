using FuzzyLib;
using System.Collections.Generic;
using System.Linq;

namespace GreenhouseCore
{
    public class FGCData
    {
        public List<FuzzySystem> FuzzySystems { get; set; }

        public FGCData(List<FuzzySystem> fuzzySystems) {
            FuzzySystems = fuzzySystems;
        }
    }
}
