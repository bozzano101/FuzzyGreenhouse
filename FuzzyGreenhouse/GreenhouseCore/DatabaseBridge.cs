using MySql.Data.MySqlClient;
using System;

namespace GreenhouseCore
{
    class DatabaseBridge
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        private string ConnectionString { get; set; }

        public DatabaseBridge(string server, string database, string userName, string password)
        {
            Server = server;
            Database = database;
            Username = userName;
            Password = password;

            ConnectionString = $"Server={server}; Database={database}; Uid={userName}; Pwd={password}";
        }

        public void ConnectToDatabase() {
            var connection = new MySqlConnection(ConnectionString);
            
            try
            {
                connection.Open();

                var command = new MySqlCommand("SELECT * FROM fuzzygreenhouse.set;", connection);
                var result = command.ExecuteReader();
                while(result.Read())
                {
                    Console.WriteLine($"{result[0]} - {result[1]}");
                }

                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
            }
        }
    }
}
