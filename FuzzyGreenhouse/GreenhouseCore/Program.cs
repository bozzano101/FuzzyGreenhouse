using System;

namespace GreenhouseCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbBridge = new DatabaseBridge("localhost", "fuzzygreenhouse", "bozzano101", "Oyoneoyone1304");
            var data = dbBridge.FetchData();

            FuzzyLib.FuzzySystem sistem = new FuzzyLib.FuzzySystem(data.FuzzyInputSets, data.FuzzyOutputSets, data.FuzzyRules);
            sistem.ChangeSetMuValue(9, null, "Potrosnja");
            sistem.ChangeSetMuValue(8, null, "Pouzdanost");
        }
    }
}
