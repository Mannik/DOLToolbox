using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.Database;
using DOL.Database.UniqueID;

namespace DOLToolbox.Services
{
    public class ItemService
    {
        public ItemTemplate GetItem(string itemId)
        {
            return DatabaseManager.Database.FindObjectByKey<ItemTemplate>(itemId) ??
                   DatabaseManager.Database.SelectObjects<ItemTemplate>("`Id_nb` = @Id",
                       new QueryParameter("@Id", itemId)).FirstOrDefault();
        }

        public async Task<List<ItemTemplate>> GetItems()
        {
            return await Task.Run(() => DatabaseManager.Database.SelectAllObjects<ItemTemplate>().ToList());
        }

        public string SaveItem(ItemTemplate item)
        {
            item.AllowUpdate = true;
            item.Dirty = true;

            if (!item.IsPersisted)
            {
                DatabaseManager.Database.AddObject(item);
                return item.ObjectId;
            }

            DatabaseManager.Database.SaveObject(item);

            return item.ObjectId;
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