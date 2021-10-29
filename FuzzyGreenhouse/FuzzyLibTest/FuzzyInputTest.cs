using FuzzyLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FuzzyLibTest
{
    [TestClass]
    public class FuzzyInputTest
    {
        [TestMethod]
        public void FuzzyInputConstructorValidTwoLists()
        {
            // Arrange
            string name = "potrosnja";
            List<float> xs = new List<float> { 3, 10 };
            List<float> ys = new List<float> { 1, 0 };
            float mi = 9;

            // Act & Assert
            var input = new FuzzyInput(name, xs, ys, mi);
            Assert.IsTrue(Math.Abs(input.Mu - 0.14285715) < 0.001);
        }

        [TestMethod]
        public void FuzzyInputConstructorValidOneList()
        {
            // Arrange
            string name = "potrosnja";

            List<Point> list = new List<Point>();
            List<float> xs = new List<float> { 3, 10 };
            List<float> ys = new List<float> { 1, 0 };

            for(int i = 0; i < xs.Count; ++i)
                list.Add(new Point(xs[i], ys[i]));

            float mi = 9;

            // Act & Assert
            var input = new FuzzyInput(name, list, mi);
            Assert.IsTrue(Math.Abs(input.Mu - 0.14285715) < 0.001);
        }

        [TestMethod]
        public void FuzzyInputListDifferentSize()
        {
            // Arrange
            string name = "potrosnja";
            List<float> xs = new List<float> { 3, 10 };
            List<float> ys = new List<float> { 1, 0, 1 };
            float mi = 9;

            // Act & Assert
            FuzzyInput input;
            Assert.ThrowsException<ArgumentException>(() => input = new FuzzyInput(name, xs, ys, mi));
        }
    
        [TestMethod]
        public void FuzzyInputValue1()
        {
            // Arrange
            string name = "potrosnja";
            List<float> xs = new List<float> { 7, 10, 12, 15 };
            List<float> ys = new List<float> { 0, 1, 1, 0 };
            float mi = 9;

            // Act & Assert
            var input = new FuzzyInput(name, xs, ys, mi);
            Assert.IsTrue(Math.Abs(input.Mu - 0.67) < 0.01);
        }

        [TestMethod] 
        public void FuzzyInputValue2()
        {
            // Arrange
            string name = "potrosnja";
            List<float> xs = new List<float> { 12, 15, 17 };
            List<float> ys = new List<float> { 0, 1, 1 };
            float mi = 9;

            // Act & Assert
            var input = new FuzzyInput(name, xs, ys, mi);
            Debug.WriteLine(input.Mu);
            Assert.AreEqual(Math.Abs(input.Mu), 0);
        }
    }
}
