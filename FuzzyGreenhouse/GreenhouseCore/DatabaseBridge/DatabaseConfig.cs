namespace GreenhouseCore
{
    public static class DatabaseConfig
    {
        public static string LocalDb { get; } = "Server=localhost; Port=3306; Database=fuzzygreenhouse; Uid=root; Pwd=Oyoneoyone1304";
        public static string TestDb { get; } = "Server=MYSQL8002.site4now.net;Database=db_a891a5_testdb;Uid=a891a5_testdb;Pwd=Oyoneoyone1304";
        public static string LiveDb { get; }
    }
}