using Core.Inventory;
using Core.Inventory.Data;
using Core.Inventory.Item;
using Core.Inventory.View;
using Core.Pool;
using Extensions;
using Loaders.Data;
using UnityEngine;

namespace Loaders
{
    public class MapLoader : ILoader
    {
        private const string ItemsOnMapFile = "ItemsOnMap";
        private readonly ItemObjectPool _pool;


        public MapLoader(ItemObjectPool pool)
        {
            _pool = pool;
        }

        public void Load()
        {
            LoadItemsInPoolAndMap(_pool);
        }

        /// <summary>
        /// Загрузка предметов на карту
        /// </summary>
        private void LoadItemsInPoolAndMap(ItemObjectPool pool)
        {
            var data = StaticLoader.LoadData<ItemsOnMap>(ItemsOnMapFile);
            foreach (var item in data.Items)
            {
                var type = item.Type.ConvertToEnum<ItemObjectType>();
                var loadedItem = new ItemData(item.Title, item.Description, item.NameIcon, type, ItemViewType.Default);
                var createdItem = pool.GetOrCreateObject(type);
                if (createdItem == null)
                {
                    Debug.LogError($"MapLoader. Item is null: {type}");
                    continue;
                }

                createdItem.Init(loadedItem,() => pool.HideObject(createdItem));
                createdItem.transform.position = item.Position.ConvertToVector3();
                createdItem.transform.eulerAngles = item.Rotation.ConvertToVector3();
            }
        }
    }
}