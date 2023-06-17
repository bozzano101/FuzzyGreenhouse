using System;
using System.Collections.Generic;

namespace FuzzyLib
{
    /// <summary>
    /// Fuzzy output class represent one fuzzy output set. It is used for describing function
    /// in 2D Cartesian plane with list of 2D Points. That function is used for us to evaluate
    /// output value how much it  belongs to this output set. We calculate centroid point for each
    /// output set. Fuzzy logic differs from traditional , binary logic and belonging function here 
    /// is not true(1) or false(0) value, instead it is a number from range [0,1].
    /// </summary>
    public class FuzzyOutput
    {
        /// <summary>
        /// Constructor function for FuzzyOutput class
        /// </summary>
        /// <param name="id">Id from database</param>
        /// <param name="name">Describe string of function</param>
        /// <param name="points">List of edge points</param>
        public FuzzyOutput(int id, string name, List<Point> points)
        {
            Id = id;
            Name = name;
            Points = points;
            Value = 0;
            WeightedAverage = CalculateWeightedAverage();
        }

        /// <summary>
        ///  Constructor function for FuzzyOutput class
        /// </summary>
        /// <param name="id">Id from database</param>
        /// <param name="name">Describe string of function</param>
        /// <param name="xs">List of X coordinates of edge points</param>
        /// <param name="ys">List of Y coordinates of edge points</param>
        public FuzzyOutput(int id, string name, List<float> xs, List<float> ys)
        {
            if (xs.Count != ys.Count)
                throw new ArgumentException("List of X coordinates and list of Y coordinates are not same length.");

            Points = new List<Point>();
            for (int i = 0; i < xs.Count; ++i)
                Points.Add(new Point(xs[i], ys[i]));

            Id = id;
            Name = name;
            Value = 0;
            WeightedAverage = CalculateWeightedAverage();
        }

        /// <summary>
        /// Method used for calculating centroid value of this set function
        /// </summary>
        /// <returns></returns>
        public float CalculateWeightedAverage()
        {
            float average = 0;
            float n = 0;
            foreach (var point in Points)
            {
                if(point.Y == 1)
                {
                    average += point.X;
                    ++n;
                }
            }

            if (n == 0)
                throw new DivideByZeroException("List of points is empty");

            return average / n;
        }

        /// <summary>
        /// Id - field represents Id getted from database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name - field that will represent name of function. For example, in case we have car fuel consumption, it can be small, average or high
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Points - List of 2D points which represents edge points of function line
        /// </summary>
        public List<Point> Points { get; set; }

        /// <summary>
        /// Value - This field is used for getting function value for given Value number
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// WeightedAverage - This field is used to store centroid value for this set function
        /// </summary>
        public float WeightedAverage { get; set; }
    }
}
