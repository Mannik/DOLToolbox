using System.Configuration;
using MySql.Data.MySqlClient;

namespace DOLToolbox.Services
{
    public static class ConnectionStringService
    {
        private static readonly Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static MySqlConnectionStringBuilder ConnectionString { get; private set; }

        static ConnectionStringService()
        {
            GetString();
        }

        private static void GetString()
        {
            var connString = Config.ConnectionStrings.ConnectionStrings["DbContext"].ConnectionString;
            ConnectionString = new MySqlConnectionStringBuilder(connString);
        }

        public static void SetString(string userId, string password, string hostname, string database, uint port)
        {
            var connString = $"server={hostname};port={port};database={database};user id={userId};password={password};treattinyasboolean=False";

            Config.ConnectionStrings.ConnectionStrings["DbContext"].ConnectionString = connString;
            Config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");

            GetString();
        }
    }
}