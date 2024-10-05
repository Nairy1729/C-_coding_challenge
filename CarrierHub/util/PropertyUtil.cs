using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CareerHub.util
{
    public static class PropertyUtil
    {
        public class DBConfig
        {
            public ConnectionStrings ConnectionStrings { get; set; }
        }

        public class ConnectionStrings
        {
            public string LocalConnectionString { get; set; }
        }

        public static string GetConnectionString(string jsonFilePath)
        {
            try
            {
                var jsonString = File.ReadAllText(jsonFilePath);
                DBConfig dbConfig = JsonSerializer.Deserialize<DBConfig>(jsonString);

                return dbConfig.ConnectionStrings.LocalConnectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON file: " + ex.Message);
                return null;
            }
        }
    }
}
