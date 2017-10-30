using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<string> Save(List<MerchantItem> models, string itemListId)
        {
            return await Task.Run(() =>
            {
                var existingId = models.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ItemListID))?.ItemListID;

                List<MerchantItem> current = new List<MerchantItem>();
                if (!string.IsNullOrWhiteSpace(existingId))
                {
                    current = Get(existingId);
                }

                if (string.IsNullOrWhiteSpace(itemListId))
                {
                    itemListId = IDGenerator.GenerateID();
                }

                // update all items to the set ID
                models
                    .Where(x => !string.IsNullOrWhiteSpace(x.ItemListID))
                    .ForEach(x => x.ItemListID = itemListId);

                // remove deleted
                current
                    .Where(x => !models.Select(s => s.ObjectId).Contains(x.ObjectId))
                    .ForEach(x => DatabaseManager.Database.DeleteObject(x));

                // add new
                models
                    .Where(x => string.IsNullOrWhiteSpace(x.ItemListID))
                    .ForEach(x =>
                    {
                        x.ItemListID = itemListId;
                        DatabaseManager.Database.AddObject(x);
                    });

                // update changed
                (from model in models
                        join existing in current on model.ObjectId equals existing.ObjectId
                        where model.ItemListID != existing.ItemListID ||
                              model.ItemTemplateID != existing.ItemTemplateID ||
                              model.PageNumber != existing.PageNumber ||
                              model.SlotPosition != existing.SlotPosition
                        select model)
                    .ToList()
                    .ForEach(x => DatabaseManager.Database.SaveObject(x));

                return itemListId;
            });
        }

        public async Task DeleteList(string listId)
        {
            var items = Get(listId);

            await Task.Run(() =>
             {
                 items.ForEach(x => DatabaseManager.Database.DeleteObject(x));
             });
        }
    }
}