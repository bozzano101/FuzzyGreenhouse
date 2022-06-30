namespace GreenhouseCore
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseBridge.ConnectionString = DatabaseConfig.TestDb;

            var x = DatabaseBridge.FetchData();
        }
    }
}
