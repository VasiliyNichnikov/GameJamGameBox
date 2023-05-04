using Core.Inventory;
using Loaders.Data.Ready;

namespace Core.Doors
{
    public class DoorKey : DoorBase
    {
        public override bool IsDisplayedHintAfterInput => true;
        public override bool IsQuestCompleted { get; protected set; } = false;

        public override bool HasHint => !_isThereKey;
        public override string Hint => _hint;

        private ItemData _neededItem;
        private bool _isThereKey; // Был ли собран ключ

        private IInventoryManager GetInventory() => _inventory ??= Game.Instance.InventoryManager;
        private IInventoryManager _inventory;
        private string _hint;

        public void Init(ItemData neededItem, string hint)
        {
            _neededItem = neededItem;
            _hint = hint;
        }

        public override void Input()
        {
            
            if (!_isThereKey && GetInventory().IsThereItem(_neededItem.ObjectType))
            {
                GetInventory().RemoveItemFromInventory(_neededItem.ObjectType);
                _isThereKey = true;
            }
            
            if (_isThereKey)
            {
                ChangeStateDoor();
            }
        }
    }
}