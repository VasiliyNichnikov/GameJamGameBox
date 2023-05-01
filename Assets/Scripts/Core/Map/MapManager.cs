using Core.Pool;
using Loaders;

namespace Core.Map
{
    public class MapManager : ILoader, IMapManager
    {
        private readonly ItemObjectPool _pool;

        public MapManager()
        {
            _pool = new ItemObjectPool();
        }

        public void Load()
        {
            CreateItemsOnScene();
        }

        private void CreateItemsOnScene()
        {
            var items = Main.Instance.Data.MapHelper.Items;
            foreach (var item in items)
            {
                var createdItem = _pool.GetOrCreateObject(item.ObjectType);
                createdItem.transform.position = item.Position;
                createdItem.transform.rotation = item.Rotation;
                createdItem.Init(item, () => _pool.HideObject(createdItem));
            }
        }
    }
}