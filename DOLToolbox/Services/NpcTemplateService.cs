using System.Linq;
using DOL.Database;

namespace DOLToolbox.Services
{
    public class NpcTemplateService
    {
        public DBNpcTemplate Get(string id)
        {
            return DatabaseManager.Database.FindObjectByKey<DBNpcTemplate>(id) ??
                   DatabaseManager.Database.SelectObjects<DBNpcTemplate>("`TemplateId` = @Id",
                       new QueryParameter("@Id", id)).FirstOrDefault();
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