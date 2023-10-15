using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.Database;
using DOL.Database.UniqueID;

namespace DOLToolbox.Services
{
    public class RewardQuestService
    {
        public async Task<DBRewardQuest> Get(string objectId)
        {
            return await Task.Run(() => DatabaseManager.Database.FindObjectByKey<DBRewardQuest>(objectId) ??
                                        DatabaseManager.Database.SelectObjects<DBRewardQuest>(
                                            DB.Column("ID").IsEqualTo(objectId)).FirstOrDefault());
        }

        public async Task<List<DBRewardQuest>> Get()
        {
            return await Task.Run(() => DatabaseManager.Database.SelectAllObjects<DBRewardQuest>().ToList());
        }

        public string Save(DBRewardQuest quest)
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
            return DatabaseManager.Database.SelectAllObjects<DBRewardQuest>()
                       .OrderByDescending(x => x.ID)
                       .Select(x => x.ID)
                       .FirstOrDefault() + 1;
        }

        public void Delete(DBRewardQuest quest)
        {
            DatabaseManager.Database.DeleteObject(quest);
        }
    }
}