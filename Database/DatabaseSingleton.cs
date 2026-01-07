using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseSingleton
    {
        private static MySqlConnection conn = null;
        private DatabaseSingleton()
        {

        }
        public static MySqlConnection GetInstance()
        {
            if (conn == null)
            {
                MySqlConnectionStringBuilder consStringBuilder = new MySqlConnectionStringBuilder();
                consStringBuilder.UserID = ReadSetting("Name");
                consStringBuilder.Password = ReadSetting("Password");
                consStringBuilder.Database = ReadSetting("Database");
                consStringBuilder.Server = ReadSetting("DataSource");
                consStringBuilder.ConnectionTimeout = 30;
                conn = new MySqlConnection(consStringBuilder.ConnectionString);

                //conn.Open();

                try
                {
                    conn.Open();
                    Console.WriteLine($"Database '{consStringBuilder.Database}' connected successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to database: " + ex.Message);
                }
            }
            return conn;
        }

        public static void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }
    }
}
