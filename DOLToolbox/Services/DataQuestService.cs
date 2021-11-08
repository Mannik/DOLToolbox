using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.Database;
using DOL.Database.UniqueID;

namespace DOLToolbox.Services
{
    public class DataQuestService
    {
        public async Task<DBDataQuest> Get(string objectId)
        {
            return await Task.Run(() => DatabaseManager.Database.FindObjectByKey<DBDataQuest>(objectId) ??
                                        DatabaseManager.Database.SelectObject<DBDataQuest>(DB.Column("ID").IsEqualTo(objectId)));
        }

        public async Task<List<DBDataQuest>> Get()
        {
            return await Task.Run(() => DatabaseManager.Database.SelectAllObjects<DBDataQuest>().ToList());
        }

        public string Save(DBDataQuest quest)
        {
            if (!quest.IsPersisted)
            {
                quest.ObjectId = IDGenerator.GenerateID();
                DatabaseManager.Database.AddObject(quest);
                return quest.ObjectId;
            }

            DatabaseManager.Database.SaveObject(quest);
            return quest.ObjectId;
        }

        public int GetNextSpellId()
        {
            return DatabaseManager.Database.SelectAllObjects<DBDataQuest>()
                       .OrderByDescending(x => x.ID)
                       .Select(x => x.ID)
                       .FirstOrDefault() + 1;
        }

        public void Delete(DBDataQuest quest)
        {
            DatabaseManager.Database.DeleteObject(quest);
        }
    }
}