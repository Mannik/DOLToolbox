using DOL.Database;

namespace DOLToolbox.Services
{
    public class NpcTemplateService
    {
        public DBNpcTemplate Get(string id)
        {
            return DatabaseManager.Database.FindObjectByKey<DBNpcTemplate>(id);
        }

        public void Save(DBNpcTemplate template)
        {
            if (string.IsNullOrWhiteSpace(template.ObjectId))
            {
                DatabaseManager.Database.AddObject(template);
                return;
            }
            
            DatabaseManager.Database.SaveObject(template);
        }

        public void Delete(DBNpcTemplate template)
        {
            DatabaseManager.Database.DeleteObject(template);
        }
    }
}