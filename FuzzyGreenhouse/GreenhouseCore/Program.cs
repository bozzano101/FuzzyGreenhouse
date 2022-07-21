using System.Threading.Tasks;

namespace GreenhouseCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DatabaseBridge.ConnectionString = DatabaseConfig.LocalDb;

            var x = await DatabaseBridge.FetchData();
        }
    }
}
