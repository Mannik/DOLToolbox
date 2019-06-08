using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.Database;

namespace DOLToolbox.Services
{
    public class ObjectService
    {
        private static List<WorldObject> _objects;

        public WorldObject GetObject(string objectId)
        {
            return DatabaseManager.Database.FindObjectByKey<WorldObject>(objectId);
        }

        public async Task<List<WorldObject>> GetObjects()
        {
            return await Task.Run(() => _objects ?? (_objects = DatabaseManager.Database.SelectAllObjects<WorldObject>().ToList()));
        }

        public string SaveObject(WorldObject wobject)
        {
            _objects = null;
            if (!wobject.IsPersisted)
            {
                DatabaseManager.Database.AddObject(wobject);
                return wobject.ObjectId;
            }

            DatabaseManager.Database.SaveObject(wobject);
            return wobject.ObjectId;
        }

        public void DeleteObject(WorldObject wobject)
        {
            DatabaseManager.Database.DeleteObject(wobject);
        }
    }
}