using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.Database;

namespace DOLToolbox.Services
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


        public string PriceFormat(long value)
        {
            var chars = value.ToString().Reverse().ToList();

            var sb = new StringBuilder("c");
            for (int i = 0; i < chars.Count; i++)
            {
                switch (i)
                {
                    case 2:
                        sb.Insert(0, "s");
                        break;
                    case 4:
                        sb.Insert(0, "g");
                        break;
                    case 7:
                        sb.Insert(0, "p");
                        break;
                }

                sb.Insert(0, chars[i]);
            }

            var strValue = sb.ToString().Replace("00c", "").Replace("00s", "");

            return strValue;
        }

        public void Delete(ItemTemplate item)
        {
            DatabaseManager.Database.DeleteObject(item);
        }
    }
}