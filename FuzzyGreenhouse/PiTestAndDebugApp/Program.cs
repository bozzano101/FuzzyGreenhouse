using Unosquare.RaspberryIO.Abstractions;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;
using System.Device.Spi;
using Iot.Device.Adc;

namespace PiTestAndDebugApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HelloWorld!");
            SpiMCP3008();
        }

        static void LedBlink()
        {
            Pi.Init<BootstrapWiringPi>();
            var blinkingPin = Pi.Gpio[BcmPin.Gpio17];
            blinkingPin.PinMode = GpioPinDriveMode.Output;

            var isOn = false;
            for (var i = 0; i < 20; i++)
            {
                isOn = !isOn;
                blinkingPin.Write(isOn);
                Thread.Sleep(500);
            }
        }

        static void SpiMCP3008()
        {
            var hardwareSpiSettings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 1000000
            };

            using (SpiDevice spi = SpiDevice.Create(hardwareSpiSettings))
            using (Mcp3008 mcp = new Mcp3008(spi))
            {
                while (true)
                {
                    string word = "";
                    for(int i = 0; i < 5; ++i)
                    {
                        double value = mcp.Read(i);
                        value = value / 10.24;
                        value = Math.Round(value);
                        word += $"{value}%     ";
                        Thread.Sleep(100);
                    }
                    Console.WriteLine(word);
                }
            }
        }


    }
}
