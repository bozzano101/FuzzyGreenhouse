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