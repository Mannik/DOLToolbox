using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.Database;
using DOL.Database.UniqueID;

namespace DOLToolbox.Services
{
    public class RewardQuestService
    {
        public async Task<DBDQRewardQ> Get(string objectId)
        {
            return await Task.Run(() => DatabaseManager.Database.FindObjectByKey<DBDQRewardQ>(objectId) ??
                                        DatabaseManager.Database.SelectObjects<DBDQRewardQ>("`ID` = @Id",
                                            new QueryParameter("@Id", objectId)).FirstOrDefault());
        }

        public async Task<List<DBDQRewardQ>> Get()
        {
            return await Task.Run(() => DatabaseManager.Database.SelectAllObjects<DBDQRewardQ>().ToList());
        }

        public string Save(DBDQRewardQ quest)
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
            return DatabaseManager.Database.SelectAllObjects<DBDQRewardQ>()
                       .OrderByDescending(x => x.ID)
                       .Select(x => x.ID)
                       .FirstOrDefault() + 1;
        }

        public void Delete(DBDQRewardQ quest)
        {
            DatabaseManager.Database.DeleteObject(quest);
        }
    }
}