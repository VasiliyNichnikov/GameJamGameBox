using Core.Inventory.Data;
using Core.Inventory.Item;
using Core.Inventory.View;

namespace Core.Inventory.Builders
{
    public class ItemFlashlightData : IItemData
    {
        public ItemObjectType ObjectType => ItemObjectType.Flashlight;
        public ItemViewType ViewType => ItemViewType.Default;
        
        public string Title => "Фонарь";
        public string Description => "Поможет в темноте!";
        public string NameIcon => "flashlightIcon";
    }
}