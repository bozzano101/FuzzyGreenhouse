using FuzzyLib;
using GreenhouseCore.HardwareBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenhouseCore
{
    public class Program
    {
        private static FGCData Data;
        private static FuzzySystem CarSystem;
        private static PinoutConfigurations PinoutConfiguration;
        private static bool Exit;

        private static async Task Config()
        {
            // Connect to database and fetch data for first time upon starting application
            DatabaseBridge.ConnectionString = DatabaseConfig.TestDb;
            try
            {
                Data = await DatabaseBridge.FetchData();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to database and fetch data.");
                Console.WriteLine(e.Message);
                Environment.Exit(-1);
            }

            // BEGIN TEST: Prepare PinoutConfiguration for Car example
            PinoutConfiguration = new PinoutConfigurations();
            int i = 0;
            CarSystem = Data.FuzzySystems[0];
            foreach(var inputSet in CarSystem.InputSets)
            {
                var xValues = new List<float>();
                var values = inputSet.Values.ToList();
                foreach(var value in values)
                {
                    var pointList = value.Points.ToList();
                    foreach(var point in pointList)
                        xValues.Add(point.X);
                }

                SensorInput sensorInput = new SensorInput()
                {
                    DatabaseID = inputSet.Id,
                    Name = inputSet.Name,
                    MinValue = xValues.Min(),
                    MaxValue = xValues.Max()
                };

                PinoutConfiguration.AssignPin(sensorInput, i); ++i;
            }
            // END TEST
        }

        private static void DisplayPinoutAndValues()
        {
            PinoutConfiguration.DisplayPinoutAndValues();
        } 

        public static async Task Main(string[] args)
        {
            
            await Config();

            while(!Exit)
            {
                Console.WriteLine("Available modes of operation: ");
                Console.WriteLine("     (1) Update data from AdminBoard ");
                Console.WriteLine("     (2) Display current pinout assignment with values");
                Console.WriteLine("     (3) Start Greenhouse ");
                Console.WriteLine("     (4) Stop Greenhouse ");
                Console.WriteLine("     (5) Exit ");
                Console.WriteLine();
                Console.WriteLine("Select mode: ");
                var input = Console.Read();

                switch (Convert.ToChar(input))
                {
                    case '1':
                        break;

                    case '2':
                        DisplayPinoutAndValues();
                        break;

                    case '3':
                        break;

                    case '4':
                        break;

                    case '5':
                        Exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid mode selected. Exiting");
                        Exit = true;
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
                Console.Read();
                Console.Clear();

            }
        }
    }
}
