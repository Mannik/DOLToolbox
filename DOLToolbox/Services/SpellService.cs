using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.Database;
using DOL.Database.UniqueID;

namespace DOLToolbox.Services
{
    public class SpellService
    {
        public async Task<DBSpell> Get(string objectId)
        {
            return await Task.Run(() => DatabaseManager.Database.FindObjectByKey<DBSpell>(objectId) ??
                                        DatabaseManager.Database.SelectObjects<DBSpell>("`SpellID` = @Id",
                                            new QueryParameter("@Id", objectId)).FirstOrDefault());
        }

        public async Task<List<DBSpell>> Get()
        {
            return await Task.Run(() => DatabaseManager.Database.SelectAllObjects<DBSpell>().ToList());
        }

        public string Save(DBSpell spell)
        {
            if (!spell.IsPersisted)
            {
                spell.ObjectId = IDGenerator.GenerateID();
                DatabaseManager.Database.AddObject(spell);
                return spell.ObjectId;
            }

            DatabaseManager.Database.SaveObject(spell);
            return spell.ObjectId;
        }

        public int GetNextSpellId()
        {
            return DatabaseManager.Database.SelectAllObjects<DBSpell>()
                       .OrderByDescending(x => x.SpellID)
                       .Select(x => x.SpellID)
                       .FirstOrDefault() + 1;
        }

        public void Delete(DBSpell spell)
        {
            DatabaseManager.Database.DeleteObject(spell);
        }
    }
}