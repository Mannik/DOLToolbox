using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.Database;
using DOL.Database.UniqueID;

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

        public async Task<List<DBNpcTemplate>> Get()
        {
            return await Task.Run(() => DatabaseManager.Database.SelectAllObjects<DBNpcTemplate>().ToList());
        }

        public string Save(DBNpcTemplate template)
        {
            if (!template.IsPersisted)
            {
                template.ObjectId = IDGenerator.GenerateID();
                DatabaseManager.Database.AddObject(template);
                return template.ObjectId;
            }
            
            DatabaseManager.Database.SaveObject(template);
            return template.ObjectId;
        }

        public void Delete(DBNpcTemplate template)
        {
            DatabaseManager.Database.DeleteObject(template);
        }
    }
}