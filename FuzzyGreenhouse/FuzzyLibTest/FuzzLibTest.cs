using FuzzyLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FuzzyLibTest
{
    [TestClass]
    public class FuzzLibTest
    {
        [TestMethod]
        public void FuzzLibExampleTest()
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

            Debug.WriteLine(up / down);

            Assert.IsTrue(Math.Abs(up / down - 25.26315) < 0.001);
        }
    }
}
