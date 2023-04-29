using Core.Inventory.Data;

namespace Core.Inventory
{
    public interface IInventoryManager
    {
        void AddItemInInventory(ItemData data);
    }
}