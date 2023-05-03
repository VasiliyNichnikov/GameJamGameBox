using Core.Inventory.Item;
using Core.Pool;
using Loaders;
using UnityEngine;

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


        void IMapManager.AddItemOnScene(ItemObjectType type, Vector3 position, Quaternion rotation)
        {
            var createdItem = AddItemOnScene(type, position, rotation);
            createdItem.InitForQuest();
        }

        private void CreateItemsOnScene()
        {
            var items = Main.Instance.Data.MapHelper.Items;
            foreach (var item in items)
            {
                var createdItem = AddItemOnScene(item.ObjectType, item.Position, item.Rotation);
                createdItem.Init(item, () => _pool.HideObject(createdItem));
            }
        }

        private ItemObjectBase AddItemOnScene(ItemObjectType type, Vector3 position, Quaternion rotation)
        {
            var item = _pool.GetOrCreateObject(type);
            item.transform.position = position;
            item.transform.rotation = rotation;
            return item;
        }
    }
}