using Core.Inventory.Data;
using Core.Inventory.Item;
using Core.Inventory.View;

namespace Core.Inventory.Builders
{
    public class ItemKeyData : IItemData
    {
        public ItemObjectType ObjectType => ItemObjectType.Key;
        public ItemViewType ViewType => ItemViewType.Default;
        public string Title => "Ключ";
        public string Description => "Поможет открыть дверь!";
        public string NameIcon => "keyIcon";
    }
}