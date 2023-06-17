using System;
using System.Collections.Generic;

namespace FuzzyLib
{
    /// <summary>
    /// FuzzyInput class represents one fuzzy input set. It is used for describing function
    /// in 2D Cartesian plane with list of 2D Points. That function is used for us to descirbe
    /// how much something belong to that set. Fuzzy logic differs from traditional, binary logic
    /// and belonging function here is not true(1) or false(0) x, instead it is a number from 
    /// range [0,1]
    /// </summary>
    public class FuzzyInput
    {
        /// <summary>
        /// Constructor function for FuzzyInput class
        /// </summary>
        /// <param name="id">Id getted from database</param>
        /// <param name="name">Describe string of function</param>
        /// <param name="xs">List of X coordinates of edge points</param>
        /// <param name="ys">List of Y coordinates of edge points</param>
        public FuzzyInput(int id, string name, List<float> xs, List<float> ys)
        {
            if (xs.Count != ys.Count)
                throw new ArgumentException("List of X coordinates and list of Y coordinates are not same length.");

            Points = new List<Point>();
            for (int i = 0; i < xs.Count; ++i)
                Points.Add(new Point(xs[i], ys[i]));

            Name = name;
            Id = id;
        }

        /// <summary>
        /// Constructor function for FuzzyInput class
        /// </summary>
        /// <param name="id">Id getted from database</param>
        /// <param name="name">Describe string of function</param>
        /// <param name="xs">List of X coordinates of edge points</param>
        /// <param name="ys">List of Y coordinates of edge points</param>
        /// <param name="x">X coordinate of point that we want to get function x</param>
        public FuzzyInput(int id, string name, List<float> xs, List<float> ys, float x)
            : this(id, name, xs, ys)
        {
            Value = CalculateFunctionValue(x);
        }

        /// <summary>
        /// Method used for recalculating Value x if neccessary
        /// </summary>
        /// <param name="x">Value for which we will calculate what function x is</param>
        public void RecalculateFunctionValue(float x)
        {
            Value = CalculateFunctionValue(x);
        }

        /// <summary>
        /// Method used to calculate x of function in X point. It traverse Points list and check if X point is between two edge points, 
        /// and if it's inside, it calculate appropriate Y x
        /// </summary>
        /// <param name="x">X coordinate of x that we want</param>
        /// <returns></returns>
        public float CalculateFunctionValue(float x)
        {
            if (x < Points[0].X)
                return Points[0].Y;
            if (x > Points[^1].X)
                return Points[^1].Y;
            for(int i = 0; i < Points.Count-1; ++i)
            {
                float x1 = Points[i].X;
                float x2 = Points[i+1].X;

                if ((x1 <= x) && (x <= x2))
                {
                    float y1 = Points[i].Y;
                    float y2 = Points[i + 1].Y;

                    if (y1 == y2)
                        return y2;
                    if (y1 < y2)
                        return (x - x1) / (x2 - x1);
                    return (x2 - x) / (x2 - x1);
                }
            }

            throw new ArgumentOutOfRangeException("Given number X is not valid");
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
        /// Value - This field is used for getting function x for given number
        /// </summary>
        public float Value { get; set; }
        
        
    }
}
