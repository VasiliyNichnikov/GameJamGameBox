using System.Collections.Generic;
using Core.Inventory.Data;
using Core.Inventory.Item;
using UnityEngine;

namespace Core.Inventory
{
    public class PlayerStorage
    {
        private readonly List<IItemData> _items = new List<IItemData>();

        public void AddItem(IItemData data)
        {
            if (_items.Contains(data))
            {
                Debug.LogError("Item is already in the storage");
                return;
            }

            _items.Add(data);
        }

        public void RemoveItem(IItemData data)
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