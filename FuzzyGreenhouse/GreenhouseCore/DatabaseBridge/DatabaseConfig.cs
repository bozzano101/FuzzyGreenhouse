namespace GreenhouseCore
{
    public static class DatabaseConfig
    {
        public static string LocalDb { get; } = "Server=localhost; Port=3306; Database=fuzzygreenhouse; Uid=root; Pwd=Oyoneoyone1304";
        public static string TestDb { get; } = "Server=MYSQL5044.site4now.net;Database=db_a8d31a_test;Uid=a8d31a_test;Pwd=Oyoneoyone1304";
        public static string LiveDb { get; }
    }
}