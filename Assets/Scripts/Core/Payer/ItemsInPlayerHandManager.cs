using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory;
using Core.Inventory.Item;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.Payer
{
    public class ItemsInPlayerHandManager : MonoBehaviour
    {
        [Serializable]
        private struct ItemInHand
        {
            public ItemObjectType ObjectType => _objectType;
            public ItemObjectBase Item => _item;
            
            [SerializeField] private ItemObjectType _objectType;
            [SerializeField] private ItemObjectBase _item;
        }

        [SerializeField]
        private List<ItemInHand> _items;

        private ItemData? _selectedItem;
        private IInventoryManager _inventoryManager;

        public void Init(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
            _inventoryManager.OnAddItemInInventory += OnUpdateItemInHand;
        }

        private void OnDisable()
        {
            _inventoryManager.OnAddItemInInventory -= OnUpdateItemInHand;
        }

        private void OnUpdateItemInHand(ItemData data)
        {
            // Не будем менять предмет, если в руке и так уже есть предмет
            if (_selectedItem != null)
            {
                return;
            }

            UpdateItemInHand(data);
        }

        private void UpdateItemInHand(ItemData data)
        {
            // Если предмет добавляется в руку, обновляем рукав
            if (data.AddToHand)
            {
                if (_selectedItem != null)
                {
                    ChangeStateOfSelectedItem(_selectedItem.Value.ObjectType, false);
                }

                ChangeStateOfSelectedItem(data.ObjectType, true);
            }
            
            _selectedItem = data;
        }

        private void ChangeStateOfSelectedItem(ItemObjectType type, bool state)
        {
            var selectedItem = GetItemByType(type);
            selectedItem.AddToHand();
            selectedItem.gameObject.SetActive(state);
        }

        private ItemObjectBase GetItemByType(ItemObjectType type)
        {
            return _items.FirstOrDefault(item => item.ObjectType == type).Item;
        }
    }
}