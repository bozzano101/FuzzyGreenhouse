public void RecalculateFunctionsValues(float x)
{
    Values.ForEach(e => e.CalculateFunctionValue(x));
}