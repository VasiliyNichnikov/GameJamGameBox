using System;
using Loaders.Data.Ready;

namespace Core.Inventory
{
    public interface IInventoryManager
    {
        event Action<ItemData> OnAddItemInInventory;  
        void AddItemInInventory(ItemData data);
    }
}