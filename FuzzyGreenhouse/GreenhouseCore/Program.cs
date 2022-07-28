using System.Threading.Tasks;

namespace GreenhouseCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DatabaseBridge.ConnectionString = DatabaseConfig.LocalDb;

            var databaseData = await DatabaseBridge.FetchData();
            var carSystem = databaseData.FuzzySystems[0];

            carSystem.ChangeSetMuValue(9, null, "Potrosnja");
            carSystem.ChangeSetMuValue(8, null, "Pouzdanost");

            var result = carSystem.CalculateOutput();
        }
    }
}
