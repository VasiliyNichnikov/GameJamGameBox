using System;
using Core.UI.Inventory;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryManager
    {
        public event Action<ItemData> OnAddItemInInventory;
        
        private InventoryView _inventoryView;
        private PlayerStorage _storage;

        private void Start()
        {
            _storage = new PlayerStorage();
            _inventoryView =  Main.Instance.DialogManager.ShowDialog<InventoryView>();
        }

        public void AddItemInInventory(ItemData data)
        {
            _storage.AddItem(data);
            _inventoryView.Refresh(_storage.Items);
            OnAddItemInInventory?.Invoke(data);
        }
    }
}