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