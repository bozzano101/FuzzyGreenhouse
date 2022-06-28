namespace GreenhouseCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var testBridge = new DatabaseBridge(DbEnvironment.Test);
            var localBridge = new DatabaseBridge(DbEnvironment.Local);

            var x = testBridge.FetchData();
            var y = localBridge.FetchData(); // This fetching is very slow, should be asynchronus
        }
    }
}
