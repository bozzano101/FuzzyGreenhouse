using FuzzyLib;
using GreenhouseCore.HardwareBridge;
using GreenhouseCore.WebServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GreenhouseCore
{
    public class Program
    {
        private static FGCData Data;
        private static PinoutConfigurations PinoutConfiguration;

        private static Thread webServer;

        private static async Task FetchDataForFirstTime()
        {
            try
            {
                Console.WriteLine("Fetching data from database started.");
                Data = await DatabaseBridge.FetchDataV2();
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

        private static void AssignInputDataToPins(FGCData data)
        {
            PinoutConfiguration = new PinoutConfigurations();
            Console.WriteLine("Assigning input pins started.");

            var inputSets = new List<FuzzyInputSet>();
            foreach (var fuzzySystem in data.FuzzySystems)
                inputSets.AddRange(fuzzySystem.InputSets);

            inputSets = inputSets.Distinct().ToList();
            foreach (var inputSet in inputSets)
            {
                Console.WriteLine($"    Setting up pin for sensor: {inputSet.Name} ");

                // Found sensor boundaries
                var xValues = new List<float>();
                foreach (var value in inputSet.Values)
                    xValues.AddRange(value.Points.Select(p => p.X).ToList());

                // Create sensor
                Sensor sensorInput = new()
                {
                    DatabaseID = inputSet.Id,
                    Name = inputSet.Name,
                    MinValue = xValues.Min(),
                    MaxValue = xValues.Max()
                };

                
                while(true)
                {
                    var availablePins = PinoutConfiguration.GetAvailablePins();
                    if(string.IsNullOrEmpty(availablePins))
                    {
                        Console.WriteLine($"    There is no more available hardware pins. Proceeding with already assigned."); return;
                    }    
                    Console.WriteLine($"    Available pins are: {availablePins}");
                    var input = Console.ReadLine();
                    var result = input switch
                    {
                        "0" => 0,
                        "1" => 1,
                        "2" => 2,
                        "3" => 3,
                        "4" => 4,
                        "5" => 5,
                        _ => -1
                    };
                    var isAssigned = PinoutConfiguration.AssignInputPin(sensorInput, result);
                    if (isAssigned)
                        break;
                    Console.WriteLine($"    Wrong pin entered: {input}");
                }
            }
        }

        private static void PrintFGCDataToFile()
        {
            var output = JsonConvert.SerializeObject(Data);
            File.WriteAllText("./output.txt", output);
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
            Console.WriteLine("     (1) Localhost - RaspberryPi");
            Console.WriteLine("     (2) Remote database - Test environment (SmarterASP)");
            Console.WriteLine("     (3) HP 450 main computer");
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
            Console.WriteLine("     (0) Exit ");
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
                    StartGreenhouse();
                    break;
                case "0":
                    StopHttpServer();
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid mode selected. Select one from range [1 - 3]. 0 is for exiting app");
                    return;
            }

            Console.WriteLine();
            Console.ReadLine();
            Console.Clear();
        }

        private static void StartGreenhouse()
        {
            Console.WriteLine();

            while(true)
            {
                StringBuilder message = new();

                foreach (var system in Data.FuzzySystems)
                {
                    foreach(var input in system.InputSets)
                    {
                        var pin = PinoutConfiguration.FindInputPin(input.Id);
                        var value = PinoutConfiguration.ReadPinValueInRange(pin);
                        system.ChangeInputSetValue((float)value, input.Id);

                        message.Append($"| {input.Name.PadCenter()}: {value.ToString("000.00")} ");
                    }

                    message.Append($"| {system.OutputSet.Name.PadCenter()}: {system.CalculateOutput().ToString("000.00")} |");
                    message.AppendLine();
                }

                Console.Clear();
                Console.WriteLine(message.ToString());
                Thread.Sleep(200);
                if (Console.KeyAvailable)
                    break;

            }
        }

        private static void CreateAndStartHttpServer()
        {
            var host = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel();
                webBuilder.UseStartup<WebStartup>();
                webBuilder.UseUrls("http://*:5006");
                webBuilder.ConfigureLogging((logging) => { logging.ClearProviders(); });
            }).Build();

            webServer = new Thread(delegate () { host.Run(); });

            webServer.Start();
        }

        private static void StopHttpServer()
        {
            webServer.Interrupt();
        }

        public static async Task Main(string[] args)
        {
            Console.WriteLine("----- GreenhouseCore -----");
            Console.WriteLine("Application started.");
            Console.WriteLine();
            
            CreateAndStartHttpServer();
            SelectDatabase();
            await FetchDataForFirstTime();
            PrintFGCDataToFile();
            AssignInputDataToPins(Data);

            while(true)
            {
                OperationSelector(); 
            }
        }
    }
}
