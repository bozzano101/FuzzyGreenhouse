public void ChangeInputSetValue(float value, int? id, string name = null)
{
    InputSets.ForEach(set =>
    {
        if ((id.HasValue && set.Id == id) || (name != null && set.Name == name))
        {
            set.RecalculateFunctionsValues(value);
            return;
        }
    });
}

private void UpdateOutputValue()
{
    Rules.ForEach(rule => {
        rule.RecalculateOutputValue();
    });
}

private void ResetOutputValue()
{
    Rules.ForEach(rule => {
        rule.ResetOutputValue();
    });
}

public float CalculateOutput()
{
    UpdateOutputValue();

    float up = 0, down = 0;
    foreach(var outputValue in OutputSet.Values)
    {
        up += outputValue.Value * outputValue.WeightedAverage;
        down += outputValue.Value;
    }

    ResetOutputValue();
    return (up / down);
}