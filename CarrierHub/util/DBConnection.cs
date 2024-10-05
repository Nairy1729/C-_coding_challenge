using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CareerHub.util
{
    public static class DBConnection
    {
        private static SqlConnection connection;

        public static SqlConnection GetConnection(string jsonFilePath)
        {
            if (connection == null)
            {
                try
                {
                    string connectionString = PropertyUtil.GetConnectionString(jsonFilePath);

                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        connection = new SqlConnection(connectionString);
                        connection.Open();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Welcome to Carrier Hub!");
                        Console.ResetColor();


                    }
                    else
                    {
                        Console.WriteLine("Failed to create connection. Invalid connection string.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error establishing connection: " + ex.Message);
                }
            }
            return connection;
        }

        public static void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Connection closed successfully.");
            }
        }
    }
}
