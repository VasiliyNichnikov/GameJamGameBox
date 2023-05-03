using Core.Inventory.Item;
using Loaders.Data.Ready;

namespace Core.Quests.CollectingFlashlight
{
    public class CollectingFlashlightQuest : QuestBase
    {
        private bool _isCompletedQuest;
        
        public override QuestType Type => QuestType.CollectFlashlight;

        private void Start()
        {
            Game.Instance.InventoryManager.OnAddItemInInventory += OnAddItemInInventory;
        }

        private void OnDisable()
        {
            Game.Instance.InventoryManager.OnAddItemInInventory -= OnAddItemInInventory;
        }

        public override bool IsCompletedQuest()
        {
            return _isCompletedQuest;
        }

        private void OnAddItemInInventory(ItemData data)
        {
            if (data.ObjectType == ItemObjectType.Flashlight)
            {
                _isCompletedQuest = true;
                Manager.QuestCompleted(Message.Data);
                gameObject.SetActive(false);
            }
        }
    }
}