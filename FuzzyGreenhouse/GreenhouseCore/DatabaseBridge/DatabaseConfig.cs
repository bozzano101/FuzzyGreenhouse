namespace GreenhouseCore
{
    public static class DatabaseConfig
    {
        public static string LocalDb { get; } =  "Server=localhost; Port=3306; Database=fuzzygreenhouse; User ID=root; Password=Oyoneoyone1304";
        public static string TestDb { get; }  =  "Server=MYSQL5044.site4now.net;Database=db_a8d31a_test;User ID=a8d31a_test;Password=Oyoneoyone1304";
        public static string LiveDb { get; }  =  "Server=192.168.1.101; Port=3306; Database=fuzzygreenhouse; Uid=boris; Pwd=Oyoneoyone1304";
    }
}