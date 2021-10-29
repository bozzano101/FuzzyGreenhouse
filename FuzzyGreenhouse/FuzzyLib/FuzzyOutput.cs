using System;
using System.Collections.Generic;

namespace FuzzyLib
{
    public class FuzzyOutput
    {
        public FuzzyOutput(string name, List<Point> points)
        {
            Name = name;
            Points = points;
            Mu = 0;
            Centroid = CalculateCentroid();
        }

        public FuzzyOutput(string name, List<float> xs, List<float> ys)
        {
            // Check if arrays are same length
            if (xs.Count != ys.Count)
                throw new ArgumentException("List of X coordinates and list of Y coordinates are not same length.");

            Points = new List<Point>();
            for (int i = 0; i < xs.Count; ++i)
                Points.Add(new Point(xs[i], ys[i]));

            Name = name;
            Mu = 0;
            Centroid = CalculateCentroid();
        }

        public float CalculateCentroid()
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

        public string Name { get; set; }
        public List<Point> Points { get; set; }
        public float Mu { get; set; }
        public float Centroid { get; set; }
    }
}
