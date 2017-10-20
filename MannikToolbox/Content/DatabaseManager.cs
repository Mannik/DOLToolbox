using System.Collections.Generic;
using DOL.Database;
using DOL.Database.Connection;
using DOL.Database.Handlers;
using MySql.Data.MySqlClient;
using MannikToolbox.Services;

namespace MannikToolbox
{
    public class DatabaseManager
    {
        private static readonly object Lock = new object();
        private static MySqlConnection m_testConnection;
        private static MySqlConnection TestConnection
        {
            get { return m_testConnection; }
            set { m_testConnection = value; }
        }
        private static IObjectDatabase m_database;
        internal static IObjectDatabase Database
        {
            get
            {
                lock (Lock)
                {
                    if (m_database != null)
                    {
                        return m_database;
                    }

                    SetDatabaseConnection();

                    return m_database;
                }
            }
            private set { m_database = value; }
        }

        public static void SetDatabaseConnection()
        {
            string connectionString;

            //Create a connection string using the string builder
            MySqlConnectionStringBuilder sb = ConnectionStringService.ConnectionString;
            sb.ConnectionTimeout = 2;
            connectionString = sb.ConnectionString;
            
            //Set the Database object
            Database = new MySQLObjectDatabase(connectionString);
			Database.RegisterDataObject(typeof(Account));
			Database.RegisterDataObject(typeof(BugReport));
			Database.RegisterDataObject(typeof(DBAppeal));
			Database.RegisterDataObject(typeof(DOLCharacters));
			Database.RegisterDataObject(typeof(DBDataQuest));
            Database.RegisterDataObject(typeof(CharacterXDataQuest));
            Database.RegisterDataObject(typeof(Mob));
            Database.RegisterDataObject(typeof(DBNpcTemplate));
            Database.RegisterDataObject(typeof(DBSpell));
            Database.RegisterDataObject(typeof(ItemTemplate));
            Database.RegisterDataObject(typeof(DBLineXSpell));
            Database.RegisterDataObject(typeof(Race));
            Database.RegisterDataObject(typeof(DBRegions));
            Database.RegisterDataObject(typeof(Zones));
            TestConnection = new MySqlConnection(sb.ConnectionString);

            try { TestConnection.Open(); }
            catch { }

            if (TestConnection.State == System.Data.ConnectionState.Open)
            {
                //   MessageBox.Show("Connection to the database was successful!");
                TestConnection.Close();
            }
            else
            {
                //   MessageBox.Show("Could not connect to the database! Check your credentials!");
            }
        }
    }
}
