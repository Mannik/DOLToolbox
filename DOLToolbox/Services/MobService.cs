using DOL.Database;

namespace DOLToolbox.Services
{
    public class MobService
    {
        public Mob GetMob(string mobId)
        {
            return DatabaseManager.Database.FindObjectByKey<Mob>(mobId);
        }

        public string SaveMob(Mob mob)
        {
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