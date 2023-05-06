using System.Collections.Generic;
using System.Linq;
using Core.Doors;
using Core.Inventory.Item;
using Core.LightLogic;
using Core.Pool;
using Core.Rooms;
using DataHelpers;
using JetBrains.Annotations;
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
        private List<LightForTrigger> _lightsForTrigger;
        private readonly Transform _playerTransform;
        private BrokenDoor _brokenDoor;

        public MapManager(MapDataHelper mapData, Transform playerTransform)
        {
            _mapData = mapData;
            _playerTransform = playerTransform;
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
            _lightsForTrigger = Object.FindObjectsByType<LightForTrigger>(FindObjectsSortMode.None).ToList();
            _brokenDoor = Object.FindObjectOfType<BrokenDoor>();
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

        public ItemObjectBase AddItemOnScene(int itemId, Vector3 position, Vector3 scale, Quaternion rotation,
            bool ignoreForRaise = false)
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

        public LightForTrigger GetClosestLightToPlayer(float radius)
        {
            if (_lightsForTrigger.Count == 0)
            {
                return null;
            }

            LightForTrigger usedLight = null;
            foreach (var light in _lightsForTrigger)
            {
                var selectedRadius = Vector3.Distance(light.transform.position, _playerTransform.position);
                if (selectedRadius <= radius)
                {
                    usedLight = light;
                    break;
                }
            }

            if (usedLight != null)
            {
                _lightsForTrigger.Remove(usedLight);
            }

            return usedLight;
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

        public void DestroyBlockTrigger()
        {
            _brokenDoor.DestroyDoor();
        }

        /// <summary>
        /// Создание дверей на сцене
        /// </summary>
        private void InitDoorsOnScene()
        {
            var doors = Main.Instance.Data.DoorHelper.Doors;
            foreach (var doorData in doors)
            {
                if (doorData.Data.DoorType != DoorType.Key)
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