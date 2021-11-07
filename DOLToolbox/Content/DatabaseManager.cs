using System;
using DOL.Database;
using DOL.Database.Handlers;
using DOLToolbox.Services;

namespace DOLToolbox
{
    public class DatabaseManager
    {
        private static readonly object Lock = new object();
        public static Type[] RegisteredObjects = {
            typeof(Account),
            typeof(BugReport),
            typeof(DBAppeal),
            typeof(DOLCharacters),
            typeof(DBDataQuest),
            typeof(CharacterXDataQuest),
            typeof(Mob),
            typeof(DBNpcTemplate),
            typeof(DBSpell),
            typeof(ItemTemplate),
            typeof(DBLineXSpell),
            typeof(Race),
            typeof(DBRegions),
            typeof(MerchantItem),
            typeof(Zones),
            typeof(NPCEquipment),
            typeof(MobXLootTemplate),
            typeof(LootTemplate),
            typeof(WorldObject)
        };

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

        public static void SetDatabaseConnection(IProgress<int> progress = null)
        {
            //Create a connection string using the string builder
            var dbConfig = new DbConfig(ConnectionStringService.DbConfig.ConnectionString);
            dbConfig.SetOption("ConnectionTimeout", "2");
            var connectionString = dbConfig.ConnectionString;

            Database = new MySQLObjectDatabase(connectionString);

            for (int i = 0; i < RegisteredObjects.Length; i++)
            {
                Database.RegisterDataObject(RegisteredObjects[i]);
                var perc = ((i + 1) / (decimal)RegisteredObjects.Length) * 100;
                progress?.Report((int)perc);
            }
        }
    }
}
