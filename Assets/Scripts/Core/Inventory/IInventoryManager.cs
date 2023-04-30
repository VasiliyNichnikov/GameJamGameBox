using System;
using Core.Inventory.Data;

namespace Core.Inventory
{
    public interface IInventoryManager
    {
        event Action<ItemData> OnAddItemInInventory;  
        void AddItemInInventory(ItemData data);
    }
}