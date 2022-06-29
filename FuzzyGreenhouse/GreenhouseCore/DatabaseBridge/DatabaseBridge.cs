using FuzzyLib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GreenhouseCore
{
    public class DatabaseBridge
    {
        private string ConnectionString { get; set; }
        private DbEnvironment DbEnvironment { get; set; }

        public DatabaseBridge(DbEnvironment environment)
        {
            DbEnvironment = environment;

            switch (DbEnvironment)  
            {
                case DbEnvironment.Local:
                    ConnectionString = DatabaseConfig.LocalDb; break;
                case DbEnvironment.Test:
                    ConnectionString = DatabaseConfig.TestDb; break;
                case DbEnvironment.Live:
                    ConnectionString = DatabaseConfig.LiveDb; break;
                default:
                    throw new ArgumentException($"Database environment is not supported");
            }
        }

        public FGCData FetchData()
        {
            var connection = new MySqlConnection(ConnectionString);

            try
            {
                connection.Open();

                var commandSet = new MySqlCommand("SELECT * FROM `set`", connection);
                var resultSet = commandSet.ExecuteReader();

                var fuzzyInputSets = new List<FuzzyInputSet>();
                var fuzzyOutputSets = new List<FuzzyOutputSet>();
                var fuzzyRules = new List<FuzzyRules>();

                while (resultSet.Read())
                {
                    int setId = Convert.ToInt32(resultSet[0]);
                    string setName = Convert.ToString(resultSet[1]);
                    string setType = Convert.ToBoolean(resultSet[2]) ? "Output" : "Input";

                    if (setType == "Input")
                        fuzzyInputSets.Add(new FuzzyInputSet(setId, setName));
                    else
                        fuzzyOutputSets.Add(new FuzzyOutputSet(setId, setName));
                }

                resultSet.Close();

                var fuzzySets = new List<dynamic>();
                fuzzySets.AddRange(fuzzyInputSets);
                fuzzySets.AddRange(fuzzyOutputSets);

                foreach (var set in fuzzySets)
                {
                    var commandValues = new MySqlCommand($"SELECT * FROM `value` WHERE SetID = {set.Id};", connection);
                    var resultValues = commandValues.ExecuteReader();

                    while (resultValues.Read())
                    {
                        var valueId = Convert.ToInt32(resultValues[0]);
                        var valueName = Convert.ToString(resultValues[1]);
                        var valueXCoords = Convert.ToString(resultValues[2]).Split(',').Select(float.Parse).ToList();
                        var valueYCoords = Convert.ToString(resultValues[3]).Split(',').Select(float.Parse).ToList();

                        if (set is FuzzyInputSet)
                            set.AddValue(new FuzzyInput(valueId, valueName, valueXCoords, valueYCoords));
                        else
                            set.AddValue(new FuzzyOutput(valueId, valueName, valueXCoords, valueYCoords));
                    }

                    resultValues.Close();
                }

                var commandRules = new MySqlCommand($"SELECT * FROM `rule`;", connection);
                var resultRules = commandRules.ExecuteReader();

                while (resultRules.Read())
                {
                    var logicOperator = Convert.ToInt32(resultRules[1]) == 0 ? LogicOperator.AND : LogicOperator.OR;
                    var input1Id = Convert.ToInt32(resultRules[2]);
                    var input2Id = Convert.ToInt32(resultRules[3]);
                    var outputId = Convert.ToInt32(resultRules[4]);

                    FuzzyInput input1 = null;
                    FuzzyInput input2 = null;
                    FuzzyOutput output = null;

                    foreach (var set in fuzzySets)
                        foreach (var value in set.Values)
                        {
                            if (value.Id == input1Id)
                                input1 = value;
                            if (value.Id == input2Id)
                                input2 = value;
                            if (value.Id == outputId)
                                output = value;
                        }

                    fuzzyRules.Add(new FuzzyRules(input1, input2, output, logicOperator));
                }

                resultRules.Close();
                connection.Close();

                FGCData data = new FGCData(fuzzyInputSets, fuzzyOutputSets, fuzzyRules);
                return data;
            }
            catch (MySqlException ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Connection to MySql database failed. Reason: Cannot connect to server.");
                        break;
                    case 1045:
                        Console.WriteLine("Connection to MySql database failed. Reason: Wrong username, password or url.");
                        break;
                }
                connection.Close();

                throw new Exception("Failed to fetch data from server", ex);
            }
        }
    }
}
