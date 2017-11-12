using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.Database; 
using DOLToolbox.Models;

namespace DOLToolbox.Services
{
    public class LootTemplateService
    {
        public async Task<LootTemplateModel> Get(string templateId)
        {
            return await Task.Run(() =>
            {
                var template = DatabaseManager.Database.FindObjectByKey<MobXLootTemplate>(templateId);
                if (template == null)
                {
                    return null;
                }

                var model = new LootTemplateModel
                {
                    MobXLootTemplate = template,
                    LootTemplates = DatabaseManager.Database
                        .SelectObjects<LootTemplate>("`TemplateName` = @TemplateName",
                            new QueryParameter("@TemplateName", template.LootTemplateName))
                        .ToList()
                };

                return model;
            });
        }

        public async Task<List<MobXLootTemplate>> Get()
        {
            return await Task.Run(() => DatabaseManager.Database.SelectAllObjects<MobXLootTemplate>().ToList());
        }

        public async Task Remove(MobXLootTemplate template)
        {
            if (string.IsNullOrWhiteSpace(template.ObjectId))
            {
                return;
            }

            await Task.Run(() =>
            {
                DatabaseManager.Database
                    .SelectObjects<LootTemplate>("`TemplateName` = @TemplateName",
                        new QueryParameter("@TemplateName", template.LootTemplateName))
                    .ToList()
                    .ForEach(x => DatabaseManager.Database.DeleteObject(x));

                DatabaseManager.Database.DeleteObject(template);
            });
        }

        public async Task Remove(LootTemplate loot)
        {
            if (string.IsNullOrWhiteSpace(loot.ObjectId))
            {
                return;
            }

            await Task.Run(() =>
            {
                DatabaseManager.Database.DeleteObject(loot);
            });
        }

        public async Task<string> Save<T>(T obj)
            where T: DataObject
        {
            await Task.Run(() =>
            {
                if (!obj.IsPersisted)
                {
                    DatabaseManager.Database.AddObject(obj);
                }
                else
                {
                    DatabaseManager.Database.SaveObject(obj);
                }
            });
            return obj.ObjectId;
        }
    }
}