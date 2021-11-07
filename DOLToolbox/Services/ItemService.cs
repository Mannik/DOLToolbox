using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.Database;

namespace DOLToolbox.Services
{
    public class ItemService
    {
        private static List<ItemTemplate> _items;

        public ItemTemplate GetItem(string itemId)
        {
            return DatabaseManager.Database.FindObjectByKey<ItemTemplate>(itemId) ??
                   DatabaseManager.Database.SelectObject<ItemTemplate>(DB.Column("Id_nb").IsEqualTo(itemId));
        }

        public async Task<List<ItemTemplate>> GetItems()
        {
            return await Task.Run(() => _items ?? (_items = DatabaseManager.Database.SelectAllObjects<ItemTemplate>().ToList()));
        }

        public string SaveItem(ItemTemplate item)
        {
            _items = null;
            item.AllowUpdate = true;
            item.Dirty = true;

            if (!item.IsPersisted)
            {
                DatabaseManager.Database.AddObject(item);

                return item.Id_nb;
            }

            DatabaseManager.Database.SaveObject(item);

            return item.Id_nb;
        }

        public bool UpdateId(string oldId, string newId, string objectId)
        {
            var item = DatabaseManager.Database.SelectObject<ItemTemplate>(DB.Column("Id_nb").IsEqualTo(newId));

            if (item != null)
            {
                return false;
            }

#pragma warning disable CS0618 // Type or member is obsolete
            DatabaseManager.Database.ExecuteNonQuery($"UPDATE ItemTemplate SET `Id_nb` = '{newId}' WHERE `Id_nb` = '{oldId}'");
#pragma warning restore CS0618 // Type or member is obsolete

            DatabaseManager.Database.UpdateInCache<ItemTemplate>(objectId);

            return true;
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