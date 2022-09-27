using FuzzyLib;
using GreenhouseCore.HardwareBridge;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GreenhouseCore
{
    public class Program
    {
        private static FGCData Data;
        private static FuzzySystem CarSystem;
        private static PinoutConfigurations PinoutConfiguration;

        private static async Task FetchDataForFirstTime()
        {
            try
            {
                Console.WriteLine("Fetching data from database started.");
                Data = await DatabaseBridge.FetchData();
                Console.WriteLine("Fetching data from database finished.");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to database and fetch data.");
                Console.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }

        private static void TEST_CAR()
        {            
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

        private static void SelectDatabase()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------- Select database ----------------------");
            Console.WriteLine();
            Console.WriteLine("Choose database connection string: ");
            Console.WriteLine("     (1) Localhost");
            Console.WriteLine("     (2) SmarterASP - Test");
            Console.WriteLine("     (3) HP Probook 450 - Local");
            Console.WriteLine();
            Console.WriteLine("Select mode: ");
            var input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    DatabaseBridge.ConnectionString = DatabaseConfig.LocalDb;
                    break;

                case "2":
                    DatabaseBridge.ConnectionString = DatabaseConfig.TestDb;
                    break;

                case "3":
                    DatabaseBridge.ConnectionString = DatabaseConfig.LiveDb;
                    break;

                default:
                    Console.WriteLine("Invalid database selected. Exiting");
                    Environment.Exit(-1);
                    break;
            }

            Console.WriteLine();
            Console.WriteLine($"Selected connection string: {DatabaseBridge.ConnectionString}");
            Console.WriteLine("--------------------------------------------------------------");
            
            Console.WriteLine();
        }

        private static void OperationSelector()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------- Select operation ----------------------");
            Console.WriteLine();

            Console.WriteLine("Select mode of operation: ");
            Console.WriteLine("     (1) Update data from AdminBoard ");
            Console.WriteLine("     (2) Display current pinout assignment with values");
            Console.WriteLine("     (3) Start Greenhouse ");
            Console.WriteLine("     (4) Stop Greenhouse ");
            Console.WriteLine("     (5) Exit ");
            Console.WriteLine();
            Console.WriteLine("Choose mode: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    break;
                case "2":
                    DisplayPinoutAndValues();
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid mode selected. Select one from range [1 - 4]. 5 is for exiting app");
                    return;
            }

            Console.WriteLine();
            Console.ReadLine();
            Console.Clear();
        }

        public static async Task Main(string[] args)
        {
            Console.WriteLine("----- GreenhouseCore -----");
            Console.WriteLine("Application started.");
            Console.WriteLine();

            SelectDatabase();
            await FetchDataForFirstTime();
            TEST_CAR();

            while(true)
            {
                OperationSelector(); 
            }
        }
    }
}
