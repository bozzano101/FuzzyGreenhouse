public void RecalculateOutputValue()
{
    if (Operator == LogicOperator.AND)
        Output.Value = Math.Max(Output.Value, Math.Min(Input1.Value, Input2.Value));
    else
        Output.Value = Math.Max(Output.Value, Math.Max(Input1.Value, Input2.Value));
}