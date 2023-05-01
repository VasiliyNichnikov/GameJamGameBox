using Core.Inventory.Item;
using Core.UI;
using Core.UI.Inventory;
using UnityEngine;

namespace Core.Inventory
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] private Transform _itemObjectParent;
        private ItemStorage _storage;

        public void Init(ItemStorage storage)
        {
            _storage = storage;
        }

        public ItemObjectBase CreateItemObject(ItemObjectType type)
        {
            var prefab = _storage.GetPrefabByType(type);
            if (prefab == null)
            {
                Debug.LogError($"ItemFactory. Not found item prefab with type: {type}");
                return null;
            }

            var createdItem = Instantiate(prefab, _itemObjectParent, false);
            return createdItem;
        }

        public ItemView CreateItemView(ItemViewType type, Transform itemViewParent)
        {
            var prefab = _storage.GetPrefabByType(type);
            if (prefab == null)
            {
                Debug.LogError($"ItemFactory. Not found item prefab with type: {type}");
                return null;
            }

            var createdItem = Instantiate(prefab, itemViewParent, false);
            return createdItem;
        }
    }
}