using FuzzyLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FuzzyLibTest
{
    [TestClass]
    public class FuzzyOutputTest
    {
        [TestMethod]
        public void FuzzyOutputConstructorOneList()
        {
            // Arrange
            string name = "srednja";

            List<Point> list = new List<Point>();
            List<float> xs = new List<float> { 1, 15, 25, 40 };
            List<float> ys = new List<float> { 0, 1, 1, 0 };

            for (int i = 0; i < xs.Count; ++i)
                list.Add(new Point(xs[i], ys[i]));

            // Act & Assert
            FuzzyOutput input = new FuzzyOutput(0, name, list);
            Debug.WriteLine(input.Mu);
        }

        [TestMethod]
        public void FuzzyOutputConstructorTwoLists()
        {
            // Arrange
            string name = "srednja";

            List<float> xs = new List<float> { 1, 15, 25, 40 };
            List<float> ys = new List<float> { 0, 1, 1, 0 };

            // Act & Assert
            FuzzyOutput input = new FuzzyOutput(0, name, xs, ys);
            Debug.WriteLine(input.Mu);
        }

        [TestMethod]
        public void FuzzyOutputConstructorDifferentListSize()
        {
            // Arrange
            string name = "srednja";

            List<float> xs = new List<float> { 1, 15, 25, 40 };
            List<float> ys = new List<float> { 0, 1, 1, 0, 1 };

            // Act & Assert
            FuzzyOutput output;
            Assert.ThrowsException<ArgumentException>(() => output = new FuzzyOutput(0, name, xs, ys));
        }

        [TestMethod]
        public void FuzzyOutputValue()
        {
            // Arrange
            string name = "srednja";

            List<float> xs = new List<float> { 1, 15, 25, 40 };
            List<float> ys = new List<float> { 0, 1, 1, 0 };

            // Act & Assert
            FuzzyOutput input = new FuzzyOutput(0, name, xs, ys);
            Assert.AreEqual(input.CalculateCentroid(), 20);
        }
    }
}
