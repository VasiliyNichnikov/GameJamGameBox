using Core.Inventory.Item;
using Core.Inventory.View;

namespace Core.Inventory.Data
{
    public interface IItemData
    {
        ItemObjectType ObjectType { get; } 
        ItemViewType ViewType { get; }
        string Title { get; }
        string Description { get; }
        string NameIcon { get; }
    }
}