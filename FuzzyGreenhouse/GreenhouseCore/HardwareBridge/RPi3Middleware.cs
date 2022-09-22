using Iot.Device.Adc;
using System;
using System.Device.Spi;

namespace GreenhouseCore.HardwareBridge
{
    public static class RPi3Middleware
    {
        public static SpiConnectionSettings spiConnectionSettings { get; private set; }
        public static SpiDevice spiDevice { get; private set; }
        public static Mcp3008 adConverter { get; private set; }

        private const int clkFrequency = 1000000;

        static RPi3Middleware() 
        { 
            spiConnectionSettings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = clkFrequency
            };

            spiDevice = SpiDevice.Create(spiConnectionSettings);

            adConverter = new Mcp3008(spiDevice);
        }

        public static double ReadValueFromADConverter(int pinNumber)
        {
            // Method will return number from range [0,100] which represent
            // percentage of potentiometer

            if(pinNumber < 0 || pinNumber > 5)
            {
                throw new ArgumentException("Invalid pin number. Channels available: [0,1,2,3,4,5]");
            }

            double value = adConverter.Read(pinNumber);
            value /= 10.24;
            value = Math.Round(value);

            return value;
        }



    }
}
