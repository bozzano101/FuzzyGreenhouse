using AdminBoard.Models.FuzzyGreenHouse;
using System.Collections.Generic;

namespace AdminBoard.Data
{
    public static class ValueObjects
    {
        public static Value DisabledValue()
        {
            return new Value
            {
                ValueID = 16,
                Name = "Disabled",
                XCoords = new List<float> { 0, 1 },
                YCoords = new List<float>() { 0, 1},
                SetID = 1
            };
        }
    }
}
