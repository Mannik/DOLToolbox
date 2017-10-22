using DOL.Database;

namespace MannikToolbox.Services
{
    public class ItemService
    {
        public ItemTemplate GetItem(string itemId)
        {
            return DatabaseManager.Database.FindObjectByKey<ItemTemplate>(itemId);
        }

        public void SaveItem(ItemTemplate Item)
        {
            if (string.IsNullOrWhiteSpace(Item.Id_nb))
            {
                DatabaseManager.Database.AddObject(Item);
                return;
            }

            DatabaseManager.Database.SaveObject(Item);
        }
    }
}