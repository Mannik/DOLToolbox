using System.Collections.Generic;
using System.Linq;
using DOL.Database;
using DOL.Database.UniqueID;
using DOL.GS;

namespace MannikToolbox.Services
{
    public class MerchantItemService
    {
        public List<MerchantItem> Get(string id)
        {
            return DatabaseManager.Database
                .SelectObjects<MerchantItem>("`ItemListID` = @ItemListID", new QueryParameter("@ItemListID", id))
                .ToList();
        }

        public string Save(List<MerchantItem> models)
        {
            var templateId = models.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ItemListID))?.ItemListID;

            List<MerchantItem> current = new List<MerchantItem>();
            if (!string.IsNullOrWhiteSpace(templateId))
            {
                current = Get(templateId);
            }
            else
            {
                templateId = IDGenerator.GenerateID();
            }

            // remove deleted
            current
                .Where(x => !models.Select(s => s.ObjectId).Contains(x.ObjectId))
                .ForEach(x => DatabaseManager.Database.DeleteObject(x));

            // add new
            models
                .Where(x => string.IsNullOrWhiteSpace(x.ItemListID))
                .ForEach(x =>
                {
                    x.ItemListID = templateId;
                    DatabaseManager.Database.AddObject(x);
                });

            // update changed
            (from model in models
                    join existing in current on model.ObjectId equals existing.ObjectId
                    where model.ItemTemplateID != existing.ItemTemplateID ||
                          model.PageNumber != existing.PageNumber ||
                          model.SlotPosition != existing.SlotPosition
                    select model)
                .ToList()
                .ForEach(x => DatabaseManager.Database.SaveObject(x));

            return templateId;
        }
    }
}