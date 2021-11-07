using System.Configuration;
using DOL.Database;

namespace DOLToolbox.Services
{
    public static class ConnectionStringService
    {
        private static readonly Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static DbConfig DbConfig { get; private set; } = new DbConfig();

        static ConnectionStringService()
        {
            InitializeDbConfig();
        }

        private static void InitializeDbConfig()
        {
            var connString = Config.ConnectionStrings.ConnectionStrings["DbContext"].ConnectionString;
            DbConfig.ApplyConnectionString(connString);
        }

        public static void SetString(string userId, string password, string hostname, string database, uint port)
        {
            var connString = $"server={hostname};port={port};database={database};user id={userId};password={password};treattinyasboolean=False";
            DbConfig.ApplyConnectionString(connString);

            SaveDbConfigToDisk(DbConfig);

            InitializeDbConfig();
        }

        private static void SaveDbConfigToDisk(DbConfig dbConfig)
        {
            Config.ConnectionStrings.ConnectionStrings["DbContext"].ConnectionString = dbConfig.ConnectionString;
            Config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}