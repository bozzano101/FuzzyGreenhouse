namespace GreenhouseCore
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseBridge.ConnectionString = DatabaseConfig.LocalDb;

            var x = DatabaseBridge.FetchData();
        }
    }
}
