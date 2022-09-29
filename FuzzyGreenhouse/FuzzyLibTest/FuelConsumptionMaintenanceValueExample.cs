using FuzzyLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FuzzyLibTest
{
    [TestClass]
    public class FuelConsumptionMaintenanceValueExample
    {
        [TestMethod]
        public void DynamicallyInsertedValuesInAllSets()
        {
            FuzzyInputSet Potrosnja = new FuzzyInputSet(0, "Potrosnja");
            Potrosnja.AddValue(new FuzzyInput(0, "mala", new List<float> { 3, 10 }, new List<float> { 1, 0 }));
            Potrosnja.AddValue(new FuzzyInput(0, "srednja", new List<float> { 7, 10, 12, 15 }, new List<float> { 0, 1, 1, 0 }));
            Potrosnja.AddValue(new FuzzyInput(0, "velika", new List<float> { 12, 15 }, new List<float> { 0, 1 }));

            FuzzyInputSet Pouzdanost = new FuzzyInputSet(0, "Pouzdanost");
            Pouzdanost.AddValue(new FuzzyInput(0, "visoka", new List<float> { 5, 10 }, new List<float> { 1, 0 }));
            Pouzdanost.AddValue(new FuzzyInput(0, "niska", new List<float> { 8, 15 }, new List<float> { 0, 1 }));

            FuzzyOutputSet Vrednost = new FuzzyOutputSet(0, "Vrednost");
            Vrednost.AddValue(new FuzzyOutput(0, "mala", new List<float> { 7, 15 }, new List<float> { 1, 0 }));
            Vrednost.AddValue(new FuzzyOutput(0, "srednja", new List<float> { 7, 15, 25, 40 }, new List<float> { 0, 1, 1, 0 }));
            Vrednost.AddValue(new FuzzyOutput(0, "velika", new List<float> { 25, 40 }, new List<float> { 0, 1 }));

            List<FuzzyInputSet> fuzzyInputSets = new List<FuzzyInputSet>();
            fuzzyInputSets.Add(Potrosnja); fuzzyInputSets.Add(Pouzdanost);

            List<FuzzyRules> rules = new List<FuzzyRules>();
            rules.Add(new FuzzyRules(Potrosnja.Values[0], Pouzdanost.Values[0], Vrednost.Values[2], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[0], Pouzdanost.Values[1], Vrednost.Values[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[1], Pouzdanost.Values[0], Vrednost.Values[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[1], Pouzdanost.Values[1], Vrednost.Values[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[2], Pouzdanost.Values[0], Vrednost.Values[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[2], Pouzdanost.Values[1], Vrednost.Values[0], LogicOperator.AND));

            FuzzySystem system = new FuzzySystem(fuzzyInputSets, new List<FuzzyOutputSet> { Vrednost }, rules);
            system.ChangeSetMuValue(9, null, "Potrosnja");
            system.ChangeSetMuValue(8, null, "Pouzdanost");
           
            Assert.IsTrue(Math.Abs(system.CalculateOutput() - 25.26315) < 0.001);

            system.ChangeSetMuValue(3, null, "Potrosnja");
            system.ChangeSetMuValue(4, null, "Pouzdanost");
            var x = system.CalculateOutput();
        }

        [TestMethod]
        public void PreInsertedValuesInAllSets()
        {
            FuzzyInputSet Potrosnja = new FuzzyInputSet(0, "Potrosnja");
            Potrosnja.AddValue(new FuzzyInput(0, "mala", new List<float> { 3, 10 }, new List<float> { 1, 0 }, 9));
            Potrosnja.AddValue(new FuzzyInput(0, "srednja", new List<float> { 7, 10, 12, 15 }, new List<float> { 0, 1, 1, 0 }, 9));
            Potrosnja.AddValue(new FuzzyInput(0, "velika", new List<float> { 12, 15 }, new List<float> { 0, 1 }, 9));

            FuzzyInputSet Pouzdanost = new FuzzyInputSet(0, "Pouzdanost");
            Pouzdanost.AddValue(new FuzzyInput(0, "visoka", new List<float> { 5, 10 }, new List<float> { 1, 0 }, 8));
            Pouzdanost.AddValue(new FuzzyInput(0, "niska", new List<float> { 8, 15 }, new List<float> { 0, 1 }, 8));

            FuzzyOutputSet Vrednost = new FuzzyOutputSet(0, "Vrednost");
            Vrednost.AddValue(new FuzzyOutput(0, "mala", new List<float> { 7, 15 }, new List<float> { 1, 0 }));
            Vrednost.AddValue(new FuzzyOutput(0, "srednja", new List<float> { 7, 15, 25, 40 }, new List<float> { 0, 1, 1, 0 }));
            Vrednost.AddValue(new FuzzyOutput(0, "velika", new List<float> { 25, 40 }, new List<float> { 0, 1 }));

            List<FuzzyInputSet> fuzzyInputSets = new List<FuzzyInputSet>();
            fuzzyInputSets.Add(Potrosnja); fuzzyInputSets.Add(Pouzdanost);

            List<FuzzyRules> rules = new List<FuzzyRules>();
            rules.Add(new FuzzyRules(Potrosnja.Values[0], Pouzdanost.Values[0], Vrednost.Values[2], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[0], Pouzdanost.Values[1], Vrednost.Values[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[1], Pouzdanost.Values[0], Vrednost.Values[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[1], Pouzdanost.Values[1], Vrednost.Values[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[2], Pouzdanost.Values[0], Vrednost.Values[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(Potrosnja.Values[2], Pouzdanost.Values[1], Vrednost.Values[0], LogicOperator.AND));

            FuzzySystem system = new FuzzySystem(fuzzyInputSets, new List<FuzzyOutputSet> { Vrednost }, rules);
            Assert.IsTrue(Math.Abs(system.CalculateOutput() - 25.26315) < 0.001);
        }

        // This test was used when first FuzzyLib version was created, before Set-story refactoring
        [TestMethod]
        public void ListsInsteadOfSets()
        {
            List<FuzzyInput> potrosnja = new List<FuzzyInput>();
            potrosnja.Add(new FuzzyInput(0, "mala", new List<float> { 3, 10 }, new List<float> { 1, 0 }, 9));
            potrosnja.Add(new FuzzyInput(0, "srednja", new List<float> { 7, 10, 12, 15 }, new List<float> { 0, 1, 1, 0 }, 9));
            potrosnja.Add(new FuzzyInput(0, "velika", new List<float> { 12, 15}, new List<float> { 0, 1 }, 9));

            List<FuzzyInput> pouzdanost = new List<FuzzyInput>();
            pouzdanost.Add(new FuzzyInput(0, "visoka", new List<float> { 5, 10 }, new List<float> { 1, 0 }, 8));
            pouzdanost.Add(new FuzzyInput(0, "niska", new List<float> { 8, 15 }, new List<float> { 0, 1}, 8));

            List<FuzzyOutput> vrednost = new List<FuzzyOutput>();
            vrednost.Add(new FuzzyOutput(0, "mala", new List<float> { 7, 15 }, new List<float> { 1, 0 }));
            vrednost.Add(new FuzzyOutput(0, "srednja", new List<float> { 7, 15, 25, 40 }, new List<float> { 0, 1, 1, 0 }));
            vrednost.Add(new FuzzyOutput(0, "velika", new List<float> { 25, 40 }, new List<float> { 0, 1}));

            List<FuzzyRules> rules = new List<FuzzyRules>();
            rules.Add(new FuzzyRules(potrosnja[0], pouzdanost[0], vrednost[2], LogicOperator.AND));
            rules.Add(new FuzzyRules(potrosnja[0], pouzdanost[1], vrednost[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(potrosnja[1], pouzdanost[0], vrednost[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(potrosnja[1], pouzdanost[1], vrednost[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(potrosnja[2], pouzdanost[0], vrednost[1], LogicOperator.AND));
            rules.Add(new FuzzyRules(potrosnja[2], pouzdanost[1], vrednost[0], LogicOperator.AND));

            float up = 0, down = 0;
            foreach (var v in vrednost)
            {
                up += v.Mu * v.Centroid;
                down += v.Mu;
            }

            Assert.IsTrue(Math.Abs(up / down - 25.26315) < 0.001);
        }
    }
}
