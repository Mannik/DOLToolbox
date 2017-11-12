using DOL.Database;

namespace DOLToolbox.Services
{
    public class MobService
    {
        public Mob GetMob(string mobId)
        {
            return DatabaseManager.Database.FindObjectByKey<Mob>(mobId);
        }

        public void SaveMob(Mob mob)
        {
            if (string.IsNullOrWhiteSpace(mob.ObjectId))
            {
                DatabaseManager.Database.AddObject(mob);
                return;
            }
            
            DatabaseManager.Database.SaveObject(mob);
        }

        public void DeleteMob(Mob mob)
        {
            DatabaseManager.Database.DeleteObject(mob);
        }
    }
}