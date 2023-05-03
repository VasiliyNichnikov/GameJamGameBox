using System;
using Core.Inventory.Item;
using Loaders.Data.Ready;

namespace Core.Inventory
{
    public interface IInventoryManager
    {
        event Action<ItemData> OnAddItemInInventory;  
        void AddItemInInventory(ItemData data);
        /// <summary>
        /// Удаляем предмет из инвентаря, если он есть
        /// </summary>
        void RemoveItemFromInventory(ItemObjectType type);
        /// <summary>
        /// Проверяем наличие предмета у игрока
        /// </summary>
        bool IsThereItem(ItemObjectType type);
    }
}