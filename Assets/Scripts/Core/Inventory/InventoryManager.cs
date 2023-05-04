using System;
using System.Linq;
using Core.Inventory.Item;
using Core.UI.Inventory;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryManager
    {
        private const KeyCode TabCode = KeyCode.Tab; 
        
        public event Action<ItemData> OnAddItemInInventory;
        
        private InventoryView _inventoryView;
        private PlayerStorage _storage;

        private void Start()
        {
            _storage = new PlayerStorage();
            
            _inventoryView =  Main.Instance.DialogManager.ShowDialog<InventoryView>();
            _inventoryView.CloseInventory();
            Main.Instance.InputHandler.OnInputKeyboard += ChangeStateInventory;
        }
        

        private void OnDisable()
        {
            Main.Instance.InputHandler.OnInputKeyboard -= ChangeStateInventory;
        }
        
        public void RemoveItemFromInventory(ItemObjectType type)
        {
            var selectedItem = _storage.Items.FirstOrDefault(item => item.ObjectType == type);
            if (selectedItem.Title == null)
            {
                Debug.LogWarning($"RemoveItemFromInventory: Not found item with type: {type}");
                return;
            }

            _storage.RemoveItem(selectedItem);
            _inventoryView.Refresh(_storage.Items);
        }

        public bool IsThereItem(ItemObjectType type)
        {
            return _storage.Items.Any(item => item.ObjectType == type);
        }
        
        public void AddItemInInventory(ItemData data)
        {
            _storage.AddItem(data);
            _inventoryView.Refresh(_storage.Items);
            OnAddItemInInventory?.Invoke(data);
        }

        private void ChangeStateInventory(KeyCode code)
        {
            if (code != TabCode)
            {
                return;
            }
            
            if (_inventoryView.IsOpened)
            {
                _inventoryView.CloseInventory();
            }
            else
            {
                _inventoryView.OpenInventory();
            }
        }
    }
}