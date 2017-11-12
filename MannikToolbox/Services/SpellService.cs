using System.Linq;
using System.Threading.Tasks;
using DOL.Database;

namespace DOLToolbox.Services
{
    public class SpellService
    {
        public async Task<DBSpell> Get(string objectId)
        {
            return await Task.Run(() => DatabaseManager.Database.FindObjectByKey<DBSpell>(objectId));
        }

        public void Save(DBSpell spell)
        {
            if (string.IsNullOrWhiteSpace(spell.ObjectId))
            {
                DatabaseManager.Database.AddObject(spell);
                return;
            }

            DatabaseManager.Database.SaveObject(spell);
        }

        public int GetNextSpellId()
        {
            return DatabaseManager.Database.SelectAllObjects<DBSpell>()
                       .OrderByDescending(x => x.SpellID)
                       .Select(x => x.SpellID)
                       .FirstOrDefault() + 1;
        }
    }
}