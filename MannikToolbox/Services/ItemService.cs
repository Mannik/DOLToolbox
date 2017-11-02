using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.Database;

namespace MannikToolbox.Services
{
    public class ItemService
    {
        public ItemTemplate GetItem(string itemId)
        {
            return DatabaseManager.Database.FindObjectByKey<ItemTemplate>(itemId);
        }

        public async Task<List<ItemTemplate>> GetItems()
        {
            return await Task.Run(() => DatabaseManager.Database.SelectAllObjects<ItemTemplate>().ToList());
        }

        public void SaveItem(ItemTemplate _item)
        {
            _item.AllowUpdate = true;
            _item.Dirty = true;

            if (string.IsNullOrWhiteSpace(_item.ObjectId))
            {
                DatabaseManager.Database.AddObject(_item);
                return;
            }

            DatabaseManager.Database.SaveObject(_item);
        }
    }
}