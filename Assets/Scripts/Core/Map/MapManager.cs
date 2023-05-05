using System.Linq;
using Core.Doors;
using Core.Inventory.Item;
using Core.Pool;
using DataHelpers;
using Loaders;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.Map
{
    public class MapManager : ILoader, IMapManager
    {
        private readonly MapDataHelper _mapData;
        private readonly ItemObjectPool _pool;
        private ObjectForChangesState[] _objectForChanges;

        public MapManager(MapDataHelper mapData)
        {
            _mapData = mapData;
            _pool = new ItemObjectPool();
        }

        public void LoadAwake()
        {
            CreateItemsOnScene();
            InitDoorsOnScene();
        }

        public void LoadStart()
        {
            _objectForChanges = Object.FindObjectsByType<ObjectForChangesState>(FindObjectsSortMode.None);
        }

        void IMapManager.AddItemOnScene(ItemObjectType type, Vector3 position, Quaternion rotation)
        {
            var createdItem = AddItemOnScene(type, position, rotation);
            createdItem.InitForQuest();
        }

        public ItemObjectBase AddItemOnScene(int itemId, Vector3 position, Quaternion rotation)
        {
            var data = _mapData.GetItemById(itemId);
            var createdItem = AddItemOnScene(data.ObjectType, position, rotation);
            createdItem.Init(data, () => _pool.HideObject(createdItem));
            return createdItem;
        }

        public ItemObjectBase AddItemOnScene(int itemId, Vector3 position, Vector3 scale, Quaternion rotation, bool ignoreForRaise = false)
        {
            var data = _mapData.GetItemById(itemId);
            var createdItem = AddItemOnScene(data.ObjectType, position, rotation);
            createdItem.transform.localScale = scale;
            if (ignoreForRaise)
            {
                createdItem.InitForQuest();
            }
            else
            {
                createdItem.Init(data, () => _pool.HideObject(createdItem));
            }
            return createdItem;
        }

        private void CreateItemsOnScene()
        {
            var items = Main.Instance.Data.MapHelper.Items;
            foreach (var item in items)
            {
                if (item.Position == null || item.Rotation == null)
                {
                    continue;
                }

                var createdItem = AddItemOnScene(item.ObjectType, item.Position.Value, item.Rotation.Value);
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

        public ObjectForChangesState GetObjectForChanges(string nameObject)
        {
            return _objectForChanges?.FirstOrDefault(obj => obj.name == nameObject);
        }

        /// <summary>
        /// Создание дверей на сцене
        /// </summary>
        private void InitDoorsOnScene()
        {
            var doors = Main.Instance.Data.DoorHelper.Doors;
            foreach (var doorData in doors)
            {
                if (doorData.Data.DoorType == DoorType.Quest)
                {
                    // Пока не инитим квесты
                    continue;
                }

                var doorObject = Main.Instance.DoorStorage.GetDoorByType(doorData.Data.RoomType);
                var doorKey = doorObject as DoorKey;
                if (doorKey == null)
                {
                    Debug.LogError($"Not initialized door: {doorData.Data.RoomType}");
                    continue;
                }

                var extension = doorData.GetExt<DoorKeyExtensionData>();
                if (extension == null)
                {
                    Debug.LogError($"Not extension: {doorData.Data.RoomType}");
                    continue;
                }

                doorKey.Init(_mapData.GetItemById(extension.Value.NeededItem), extension.Value.Hint);
            }
        }
    }
}