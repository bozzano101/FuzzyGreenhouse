using System;

namespace GreenhouseCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbBridge = new DatabaseBridge("localhost","fuzzygreenhouse","bozzano101","Oyoneoyone1304");
            dbBridge.ConnectToDatabase();
            Console.ReadLine();
        }
    }
}
