using System;
using System.Collections.Generic;

namespace GreenhouseCore.HardwareBridge
{
    public class PinoutConfigurations
    {
        public Dictionary<int, SensorInput> InputsPinoutConfigurations { get; set; }

        public PinoutConfigurations()
        {
            InputsPinoutConfigurations = new Dictionary<int, SensorInput>();
        }

        public void AssignPin(SensorInput sensorInput, int pinNumber)
        {
            if(InputsPinoutConfigurations.ContainsKey(pinNumber))
            {
                throw new ArgumentException($"Pin number {pinNumber} is occupied by sensor: {InputsPinoutConfigurations[pinNumber]}");
            }

            InputsPinoutConfigurations.Add(pinNumber, sensorInput);
        }

        public double ReadPinValuePercentage(int pinNumber)
        {
            return RPi3Middleware.ReadValueFromADConverter(pinNumber);
        }

        public double ReadPinValueInRange(int pinNumber)
        {
            var sensorInput = InputsPinoutConfigurations[pinNumber];
            var value = sensorInput.MinValue + 
                        (sensorInput.MaxValue - sensorInput.MinValue) * RPi3Middleware.ReadValueFromADConverter(pinNumber) / 100;
            return Math.Round(value, 2);
        }

        public void DisplayPinoutAndValues()
        {
            Console.WriteLine("{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}",
                "Pin",
                "Name",
                "MinValue",
                "MaxValue",
                "Value %",
                "Value"
            );
            Console.WriteLine("------------------------------------------------------------------------------------------");

            foreach (var pair in InputsPinoutConfigurations)
            {
                Console.WriteLine("{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}",
                    pair.Key,
                    pair.Value.Name,
                    pair.Value.MinValue,
                    pair.Value.MaxValue,
                    ReadPinValuePercentage(pair.Key),
                    ReadPinValueInRange(pair.Key)
                );
            }
        }
    }
}
