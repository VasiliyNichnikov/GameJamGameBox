using System.Collections.Generic;
using System.Collections.ObjectModel;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.Inventory
{
    public class PlayerStorage
    {
        public ReadOnlyCollection<ItemData> Items => _items.AsReadOnly();

        private readonly List<ItemData> _items = new List<ItemData>();

        public void AddItem(ItemData data)
        {
            if (_items.Contains(data))
            {
                Debug.LogError("Item is already in the storage");
                return;
            }

            _items.Add(data);
        }

        public void RemoveItem(ItemData data)
        {
            if (!_items.Contains(data))
            {
                Debug.LogError("Item is not in the storage");
                return;
            }

            _items.Remove(data);
        }
    }
}