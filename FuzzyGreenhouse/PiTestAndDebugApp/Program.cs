
using System.Diagnostics;

namespace PiTestAndDebugApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Debugger.Break();
            int x = 2 + 3;
            Console.WriteLine("hello world");
            Console.WriteLine($"{x}");
        }
    }
}
