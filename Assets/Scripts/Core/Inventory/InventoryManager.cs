using System;
using Core.Inventory.Data;
using Core.Inventory.View;
using UnityEngine;

namespace Core.Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryManager
    {
        public event Action<ItemData> OnAddItemInInventory;
        
        [SerializeField]
        private InventoryView _inventoryView;
        
        private PlayerStorage _storage;

        private void Start()
        {
            _storage = new PlayerStorage();
        }

        public void AddItemInInventory(ItemData data)
        {
            _storage.AddItem(data);
            _inventoryView.Refresh(_storage.Items);
            OnAddItemInInventory?.Invoke(data);
        }
    }
}