using System;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace WindowsFormsApp1.Database
{
    public class DatabaseSingleton
    {
        private static MySqlConnection conn = null;

        /// <summary>
        /// Private constructor to prevent external instantiation.
        /// This is required for the Singleton design pattern.
        /// </summary>
        private DatabaseSingleton()
        {

        }

        /// <summary>
        /// Returns a single instance of the MySQL database connection.
        /// If the connection does not exist, it is created and opened.
        /// </summary>
        /// <returns> Open MySqlConnection instance </returns>
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

                try
                {
                    conn.Open();
                    Console.WriteLine($"Database '{consStringBuilder.Database}' connected successfully!");

                    using (MySqlCommand command = new MySqlCommand("SET autocommit = 1;", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to database: " + ex.Message);
                }
            }
            return conn;
        }

        /// <summary>
        /// Closes and disposes the current database connection.
        /// Should be called when the application is shutting down.
        /// </summary>
        public static void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        /// <summary>
        /// Reads a value from the application configuration file (App.config / exe.config).
        /// </summary>
        /// <param name="key">Configuration key name</param>
        /// <returns>Configuration value or "Not Found" if key does not exist</returns>
        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }
    }
}
