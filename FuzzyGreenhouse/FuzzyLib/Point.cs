namespace FuzzyLib
{
    /// <summary>
    /// This class represents 2D Cartesian point
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Constructor function for 2D Cartesian point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X - Represents X coord of Point
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y - Represents Y coord of Point
        /// </summary>
        public float Y { get; set; }
    }
}
