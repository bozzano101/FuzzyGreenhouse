using FuzzyLib;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenhouseCore
{
    public class DatabaseBridge

    {
        public static string ConnectionString { get; set; }

        // For given valueId, returns FuzzyOutput name which contains given value
        public static async Task<string> GetOutputSetName(int valueId)
        {
            var connection = new MySqlConnection(ConnectionString);

            try
            {
                await connection.OpenAsync();

                var command = new MySqlCommand(
                    @"
                    SELECT s.Name FROM 
                    `rule` r JOIN `value` v ON r.OutputValueID = v.ValueID
                    JOIN `set` s ON v.SetID = s.SetID
                    WHERE r.OutputValueID = " + valueId.ToString(),
                connection);

                var result = await command.ExecuteReaderAsync();

                await result.ReadAsync();
                var name = result[0].ToString();

                if (String.IsNullOrEmpty(name))
                    throw new ArgumentException("Failed to fetch set name");

                return name;

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
                await connection.CloseAsync();

                throw new Exception("Failed to fetch data from server", ex);
            }
        }

        // For given valueId and position in rule, returns FuzzyInput name which contains given value
        public static async Task<string> GetInputSetName(int valueId, int position)
        {
            var connection = new MySqlConnection(ConnectionString);

            try
            {
                await connection.OpenAsync();
                
                var command = new MySqlCommand(
                    $@"
                    SELECT DISTINCT s.Name 
	                FROM  `rule` r, `value` v, `set` s
                    WHERE  
                            r.InputValue{position}ID = v.ValueID    
		                AND v.SetID = s.SetID
                        AND r.InputValue{position}ID = {valueId.ToString()}",
                connection);

                var result = await command.ExecuteReaderAsync();

                await result.ReadAsync();
                var name = result[0].ToString();

                if (String.IsNullOrEmpty(name))
                    throw new ArgumentException("Failed to fetch set name");

                await connection.CloseAsync();

                return name;

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
                await connection.CloseAsync();

                throw new Exception("Failed to fetch data from server", ex);
            }
        }

        public static async Task<FGCData> FetchDataV2()
        {
            var connection = new MySqlConnection(DatabaseBridge.ConnectionString);

            try
            {
                // Fetch subsystems
                var fuzzySystems = new List<FuzzySystem>();

                await connection.OpenAsync();
                var commandSubsystem = new MySqlCommand("SELECT * FROM `subsystem`", connection);
                var resultSubsystem = await commandSubsystem.ExecuteReaderAsync();

                while (await resultSubsystem.ReadAsync())
                {
                    int subSystemId      = Convert.ToInt32(resultSubsystem[0]);
                    string subSystemName = Convert.ToString(resultSubsystem[1]);
                    string subsystemDesc = Convert.ToString(resultSubsystem[2]);

                    fuzzySystems.Add(new FuzzySystem() { Name = subSystemName, Description = subsystemDesc, Id = subSystemId });
                }
                await resultSubsystem.CloseAsync();

                // Fetch sets
                var commandSet = new MySqlCommand("SELECT * FROM `set`", connection);
                var resultSet = await commandSet.ExecuteReaderAsync();

                while (await resultSet.ReadAsync())
                {
                    int setId = Convert.ToInt32(resultSet[0]);
                    string setName = Convert.ToString(resultSet[1]);
                    string setType = Convert.ToBoolean(resultSet[2]) ? "Output" : "Input";
                    int subSystemId = Convert.ToInt32(resultSet[3]);

                    var subSystem = fuzzySystems.Where(f => f.Id == subSystemId).First();
                    if (setType == "Input")
                        subSystem.InputSets.Add(new FuzzyInputSet(setId, setName));
                    else
                        subSystem.OutputSet = new FuzzyOutputSet(setId, setName);
                }
                await resultSet.CloseAsync();

                // Fetch values
                var commandValues = new MySqlCommand($"SELECT * FROM `value`", connection);
                var resultValues = await commandValues.ExecuteReaderAsync();

                while (await resultValues.ReadAsync())
                {
                    var valueId = Convert.ToInt32(resultValues[0]);
                    var valueName = Convert.ToString(resultValues[1]);
                    var valueXCoords = Convert.ToString(resultValues[2]).Split(',').Select(float.Parse).ToList();
                    var valueYCoords = Convert.ToString(resultValues[3]).Split(',').Select(float.Parse).ToList();
                    var setId = Convert.ToInt32(resultValues[4]);

                    foreach(var fuzzySystem in fuzzySystems)
                    {
                        var inputSet = fuzzySystem.InputSets.Where(f => f.Id == setId).ToList();
                        if (inputSet.Count == 1)
                        {
                            inputSet[0].AddValue(new FuzzyInput(valueId, valueName, valueXCoords, valueYCoords));
                            break;
                        }

                        if(fuzzySystem.OutputSet.Id == setId)
                        {
                            fuzzySystem.OutputSet.AddValue(new FuzzyOutput(valueId, valueName, valueXCoords, valueYCoords));
                            break;
                        }
                    };
                }
                await resultValues.CloseAsync();

                // Fetch rules
                var commandRules = new MySqlCommand($"SELECT * FROM `rule`", connection);
                var resultRules = await commandRules.ExecuteReaderAsync();

                while (await resultRules.ReadAsync())
                {
                    var logicOperator = Convert.ToInt32(resultRules[1]) == 0 ? LogicOperator.AND : LogicOperator.OR;
                    var input1Id = Convert.ToInt32(resultRules[2]);
                    var input2Id = Convert.ToInt32(resultRules[3]);
                    var outputId = Convert.ToInt32(resultRules[4]);
                    var subsystemId = Convert.ToInt32(resultRules[5]);

                    var subSystem = fuzzySystems.Where(f => f.Id == subsystemId).First();
                    FuzzyInput input1 = null;
                    FuzzyInput input2 = null;
                    FuzzyOutput output = null;

                    output = subSystem.OutputSet.Values.Where(f => f.Id == outputId).First();

                    foreach (var input in subSystem.InputSets)
                    {
                        var input1Candidate = input.Values.Where(f => f.Id == input1Id).FirstOrDefault();
                        var input2Candidate = input.Values.Where(f => f.Id == input2Id).FirstOrDefault();
                        input1 ??= input1Candidate;
                        input2 ??= input2Candidate;
                    }

                    subSystem.Rules.Add(new FuzzyRules(input1, input2, output, logicOperator));
                }
                await resultRules.CloseAsync();

                // Close SQL connection, FGC data can be created
                await connection.CloseAsync();

                return new FGCData(fuzzySystems);
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
                    case 1042:
                        Console.WriteLine("Connection to MySql database failed. Reason: Wrong username, password or url.");
                        break;
                }
                await connection.CloseAsync();

                throw new Exception("Failed to fetch data from server", ex);
            }
        }

        // Fetches all data from database: sets, values, rules
        [Obsolete("FetchDataV2 is faster, easier to read and accompany new updates to actual database. Use it instead.")]
        public static async Task<FGCData> FetchData()
        {
            var connection = new MySqlConnection(DatabaseBridge.ConnectionString);
            try
            {
                var fuzzyInputSets = new List<FuzzyInputSet>();
                var fuzzyOutputSets = new List<FuzzyOutputSet>();
                var fuzzyRules = new List<FuzzyRules>();
                var fuzzySystems = new List<FuzzySystem>();

                await connection.OpenAsync();

                var commandSet = new MySqlCommand("SELECT * FROM `set`", connection);
                var resultSet = await commandSet.ExecuteReaderAsync();

                while (await resultSet.ReadAsync())
                {
                    int setId = Convert.ToInt32(resultSet[0]);
                    string setName = Convert.ToString(resultSet[1]);
                    string setType = Convert.ToBoolean(resultSet[2]) ? "Output" : "Input";

                    if (setType == "Input")
                        fuzzyInputSets.Add(new FuzzyInputSet(setId, setName));
                    else
                        fuzzyOutputSets.Add(new FuzzyOutputSet(setId, setName));
                }

                await resultSet.CloseAsync();

                var fuzzySets = new List<dynamic>();
                fuzzySets.AddRange(fuzzyInputSets);
                fuzzySets.AddRange(fuzzyOutputSets);

                foreach (var set in fuzzySets)
                {
                    var commandValues = new MySqlCommand($"SELECT * FROM `value` WHERE SetID = {set.Id};", connection);
                    var resultValues = await commandValues.ExecuteReaderAsync();

                    while (await resultValues.ReadAsync())
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

                    await resultValues.CloseAsync();
                }

                var commandRules = new MySqlCommand($"SELECT * FROM `rule`;", connection);
                var resultRules = await commandRules.ExecuteReaderAsync();

                while (await resultRules.ReadAsync())
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

                await resultRules.CloseAsync();
                await connection.CloseAsync();

                foreach (var outputSet in fuzzyOutputSets)
                {
                    var rulesForOutputSet = new List<FuzzyRules>();
                    foreach (var rule in fuzzyRules)
                    {
                        var outputRuleName = await GetOutputSetName(rule.Output.Id);
                        if (outputSet.Name.Equals(outputRuleName))
                            rulesForOutputSet.Add(rule);
                    }

                    var inputsForThisSystem = new HashSet<FuzzyInputSet>();
                    foreach (var rule in rulesForOutputSet)
                    {
                        var input1SetName = await GetInputSetName(rule.Input1.Id, 1);
                        var input2SetName = await GetInputSetName(rule.Input2.Id, 2);

                        inputsForThisSystem.Add(
                            fuzzyInputSets.Where(e => e.Name.Equals(input1SetName)).First()
                        );
                        inputsForThisSystem.Add(
                            fuzzyInputSets.Where(e => e.Name.Equals(input2SetName)).First()
                        );
                    }

                    var fuzzySystem = new FuzzySystem()
                    {
                        InputSets = inputsForThisSystem.ToList(),
                        OutputSet = outputSet,
                        Rules = rulesForOutputSet
                    };

                    fuzzySystems.Add(fuzzySystem);
                }

                return new FGCData(fuzzySystems);
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
                    case 1042:
                        Console.WriteLine("Connection to MySql database failed. Reason: Wrong username, password or url.");
                        break;
                }
                await connection.CloseAsync();

                throw new Exception("Failed to fetch data from server", ex);
            }
        }

        // Fetches latest version date
        public static async Task<DateTime> FetchLatestVersion()
        {
            var connection = new MySqlConnection(ConnectionString);

            try
            {
                await connection.OpenAsync();

                var command = new MySqlCommand(
                    @"
                    SELECT * FROM fuzzygreenhouse.version
                    ORDER BY CreatedDate DESC
                    LIMIT 1", connection);

                var result = await command.ExecuteReaderAsync();

                await result.ReadAsync();
                var latestDate = DateTime.Parse(result[1].ToString());

                await connection.CloseAsync();

                return latestDate;
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
                await connection.CloseAsync();

                throw new Exception("Failed to fetch data from server", ex);
            }
        }
    }


}
