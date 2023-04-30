using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory;
using Core.Inventory.Data;
using Core.Inventory.Item;
using UnityEngine;

namespace Core.Payer
{
    public class PlayerManager : MonoBehaviour
    {
        [Serializable]
        private struct ItemInHand
        {
            public ItemObjectType ObjectType => _objectType;
            public GameObject Item => _item;
            
            [SerializeField] private ItemObjectType _objectType;
            [SerializeField] private GameObject _item;
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
            if (_selectedItem != null)
            {
                ChangeStateOfSelectedItem(_selectedItem.Value.ObjectType, false);
            }

            ChangeStateOfSelectedItem(data.ObjectType, true);
            _selectedItem = data;
        }

        private void ChangeStateOfSelectedItem(ItemObjectType type, bool state)
        {
            var selectedItem = GetItemByType(type);
            selectedItem.SetActive(state);
        }

        private GameObject GetItemByType(ItemObjectType type)
        {
            return _items.FirstOrDefault(item => item.ObjectType == type).Item;
        }
    }
}