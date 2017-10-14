using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOL.Database;
using DOL.Database.Connection;
using DOL.Database.Handlers;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using MannikToolbox.Services;

namespace MannikToolbox
{
    public class DatabaseManager
    {
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
                if (m_database != null)
                {
                    return m_database;
                }

                SetDatabaseConnection();

                return m_database;
            }
            private set { m_database = value; }
        }
        private static DataConnection m_dataConnection;
        internal static DataConnection DataConnection
        {
            get { return m_dataConnection; }
            private set { m_dataConnection = value; }
        }

        public static void SetDatabaseConnection()
        {
            string connectionString;

            //Create a connection string using the string builder
            MySqlConnectionStringBuilder sb = ConnectionStringService.ConnectionString;
            sb.ConnectionTimeout = 2;
            connectionString = sb.ConnectionString;

            //Set the data connection object
            DataConnection = new DataConnection(ConnectionType.DATABASE_MYSQL, connectionString);
            //Set the Database object
            Database = new MySQLObjectDatabase(m_dataConnection);
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
        public DBDataQuest GetQuestByID(int questID)
        {
            return Database.SelectObject<DBDataQuest>(Database.Escape(string.Format("ID = {0}", questID)));
        }
        public DBSpell GetSpellByID(int SpellID)
        {
            return Database.SelectObject<DBSpell>(Database.Escape(string.Format("ID = {0}", SpellID)));
        }
        public IList<DBDataQuest> GetQuestByName(string name)
        {
            return Database.SelectObjects<DBDataQuest>(Database.Escape(string.Format("Name LIKE %{0}%", name)));
        }
        public IList<DBSpell> GetSpellByName(string name)
        {
            return Database.SelectObjects<DBSpell>(Database.Escape(string.Format("Name LIKE %{0}%", name)));
        }
       
    }
}
