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

    public class PinoutConfigurations
    {
        public Dictionary<int, Sensor> InputsPinoutConfigurations { get; set; }
        public KeyValuePair<int, Sensor> OutputPinoutConfiguration { get; set; }

        public PinoutConfigurations()
        {
            InputsPinoutConfigurations = new Dictionary<int, Sensor>();
        }

        public void AssignInputPin(Sensor sensorInput, int pinNumber)
        {
            if(InputsPinoutConfigurations.ContainsKey(pinNumber))
            {
                throw new ArgumentException($"Pin number {pinNumber} is occupied by sensor: {InputsPinoutConfigurations[pinNumber]}");
            }

            InputsPinoutConfigurations.Add(pinNumber, sensorInput);
        }

        public void AssingOutputPin(Sensor sensorOutput, int pinNumber)
        {
            OutputPinoutConfiguration = new KeyValuePair<int, Sensor>(pinNumber, sensorOutput);
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
        