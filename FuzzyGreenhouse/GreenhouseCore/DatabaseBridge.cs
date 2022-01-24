using FuzzyLib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

            var list = new List<dynamic>();

            while(resultSet.Read())
            {
                int setId = Convert.ToInt32(resultSet[0]);
                string setName = Convert.ToString(resultSet[1]);
                string setType = Convert.ToBoolean(resultSet[2]) ? "Output" : "Input";

                if (setType == "Output")
                    list.Add(new FuzzyOutputSet(setId, setName));
                else
                    list.Add(new FuzzyInputSet(setId, setName));
            }

            resultSet.Close();

            foreach (var set in list)
            {
                var commandValues = new MySqlCommand($"SELECT * FROM fuzzygreenhouse.value WHERE SetID = {set.Id};", connection);
                var resultValues = commandValues.ExecuteReader();

                while (resultValues.Read())
                {
                    var valueName = Convert.ToString(resultValues[1]);
                    var valueXCoords = (Convert.ToString(resultValues[2])).Split(',').Select(float.Parse).ToList();
                    var valueYCoords = (Convert.ToString(resultValues[3])).Split(',').Select(float.Parse).ToList();

                    if (set is FuzzyInputSet)
                        set.AddValue(new FuzzyInput(valueName, valueXCoords, valueYCoords));
                    else
                        set.AddValue(new FuzzyOutput(valueName, valueXCoords, valueYCoords));
                }

                resultValues.Close();
            }

            Console.Write(" PROCESS LIST SOMEHOW ");
        }

        public void ConnectToDatabase() {
            var connection = new MySqlConnection(ConnectionString);
            
            try
            {
                connection.Open();

                FetchData(connection);

                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
            }
        }
    }
}
