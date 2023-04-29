using Core.Inventory.Item;
using Core.Inventory.View;

namespace Core.Inventory.Data
{
    public struct ItemData
    {
        public readonly ItemObjectType ObjectType;
        public readonly ItemViewType ViewType;
        public readonly string Title;
        public readonly string Description;
        public readonly string NameIcon;

        public ItemData(string title, string description, string nameIcon, ItemObjectType objectType, ItemViewType viewType)
        {
            Title = title;
            Description = description;
            NameIcon = nameIcon;
            ObjectType = objectType;
            ViewType = viewType;
        }
    }
}