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

        public void DisplayPinoutAndValues()
        {
            Console.WriteLine("{0,15}{1,15}{2,15}{3,15}{4,15}",
                "Pin",
                "Name",
                "MinValue",
                "MaxValue",
                "VALUE"
            );

            foreach (var pair in InputsPinoutConfigurations)
            {
                Console.WriteLine("{0,10}{1,10}{2,10}{3,10}{4,15}",
                    pair.Key,
                    pair.Value.Name,
                    pair.Value.MinValue,
                    pair.Value.MaxValue,
                    RPi3Middleware.ReadValueFromADConverter(pair.Key)
                );
            }
        }
    }
}
