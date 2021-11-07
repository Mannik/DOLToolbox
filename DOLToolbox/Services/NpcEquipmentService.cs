using System.Collections.Generic;
using System.Linq;
using DOL.Database;
using DOL.Database.UniqueID;

namespace DOLToolbox.Services
{
    public class NpcEquipmentService
    {
        public List<NPCEquipment> Get(string id)
        {
            return DatabaseManager.Database
                .SelectObjects<NPCEquipment>(DB.Column("TemplateID").IsEqualTo(id))
                .ToList();
        }

        public string Save(NPCEquipment template)
        {
            if (string.IsNullOrWhiteSpace(template.TemplateID))
            {
                template.TemplateID = IDGenerator.GenerateID();
            }
             
            if (string.IsNullOrWhiteSpace(template.ObjectId))
            {
                if (string.IsNullOrWhiteSpace(template.TemplateID))
                {
                    template.ObjectId = IDGenerator.GenerateID();
                }

                DatabaseManager.Database.AddObject(template);
                return template.TemplateID;
            }

            DatabaseManager.Database.SaveObject(template);
            return template.TemplateID;
        }
    }
}