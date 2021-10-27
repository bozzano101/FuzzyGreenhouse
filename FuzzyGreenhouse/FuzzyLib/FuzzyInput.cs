using System;
using System.Collections.Generic;

namespace FuzzyLib
{
    public class FuzzyInput
    {
        public FuzzyInput(string name, List<Point> points, float value)
        {
            Name = name;
            Points = points;
            Mu = CalculateMi(value);
        }

        public FuzzyInput(string name, List<float> xs, List<float> ys, float value)
        {
            // Check if arrays are same length
            if (xs.Count != ys.Count)
                throw new ArgumentException("List of X coordinates and list of Y coordinates are not same length.");

            for(int i = 0; i < xs.Count; ++i)
                Points.Add(new Point(xs[i], ys[i]));

            Name = name;
            Mu = CalculateMi(value);
        }

        public float CalculateMi(float x)
        {
            if (x < Points[0].X)
                return Points[0].Y;
            if (x > Points[Points.Count - 1].X)
                return Points[Points.Count - 1].Y;
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

        public string Name { get; set; }
        public List<Point> Points { get; set; }
        public float Mu { get; set; }
        
        
    }
}
