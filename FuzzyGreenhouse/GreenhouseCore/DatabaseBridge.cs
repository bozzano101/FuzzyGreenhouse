using FuzzyLib;
using MySql.Data.MySqlClient;
using System;
using System.Linq;

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

        private void FetchData(MySqlConnection connection)
        {
            var commandSet = new MySqlCommand("SELECT * FROM fuzzygreenhouse.set;", connection);
            var resultSet = commandSet.ExecuteReader();
            while(resultSet.Read())
            {
                var setId = Convert.ToInt32(resultSet[0]);
                var setName = Convert.ToString(resultSet[1]);
                var setType = Convert.ToBoolean(resultSet[2]) ? "Output" : "Input";
                dynamic set;

                if (setType == "Output")
                    set = new FuzzyOutputSet();
                else
                    set = new FuzzyInputSet();


                var commandValues = new MySqlCommand($"SELECT * FROM fuzzygreenhouse.value WHERE SetID = {setId};", connection);
                var resultValues = commandValues.ExecuteReader();

                while(resultValues.Read())
                {
                    var valueName = Convert.ToString(resultValues[1]);
                    var valueXCoords = (Convert.ToString(resultValues[2])).Split(',').Select(float.Parse).ToList();
                    var valueYCoords = (Convert.ToString(resultValues[3])).Split(',').Select(float.Parse).ToList();

                    if(resultSet[2].ToString() == "0")
                        set.AddValue(new FuzzyInput(valueName, valueXCoords, valueYCoords));
                    else
                        set.AddValue(new FuzzyOutput(valueName, valueXCoords, valueYCoords));
                }
            }
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
                    Console.WriteLine($"{result[0]} - {result[1]} - {result[2]}");
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
