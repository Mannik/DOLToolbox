using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.Database;

namespace DOLToolbox.Services
{
    public class MobService
    {
        private static List<Mob> _mobs;

        public Mob GetMob(string mobId)
        {
            return DatabaseManager.Database.FindObjectByKey<Mob>(mobId);
        }

        public async Task<List<Mob>> GetMobs()
        {
            return await Task.Run(() => _mobs ?? (_mobs = DatabaseManager.Database.SelectAllObjects<Mob>().ToList()));
        }

        public string SaveMob(Mob mob)
        {
            _mobs = null;
            if (!mob.IsPersisted)
            {
                DatabaseManager.Database.AddObject(mob);
                return mob.ObjectId;
            }
            
            DatabaseManager.Database.SaveObject(mob);
            return mob.ObjectId;
        }

        public void DeleteMob(Mob mob)
        {
            DatabaseManager.Database.DeleteObject(mob);
        }
    }
}