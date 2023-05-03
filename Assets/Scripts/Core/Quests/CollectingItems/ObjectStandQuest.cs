using System;
using Core.Inventory.Item;
using UnityEngine;

namespace Core.Quests.CollectingItems
{
    public class ObjectStandQuest : InteractionObjectBase
    {
        [SerializeField, Header("Точка в которо создадим поставленны объект")] 
        private Transform _pointForObject;
        
        private ItemObjectType _itemOnStand;
        public override bool IsDisplayedHintAfterInput => true;
        public override bool IsQuestCompleted { get; protected set; }
        private Action _checkAfterAddItem;

        public void Init(ItemObjectType itemOnStand, Action checkAfterAddItem)
        {
            _itemOnStand = itemOnStand;
            _checkAfterAddItem = checkAfterAddItem;
        }
        
        public override void Input()
        {
            if (IsQuestCompleted)
            {
                return;
            }
            
            var inventory = Game.Instance.InventoryManager;
            if (inventory.IsThereItem(_itemOnStand))
            {
                // Порядок важен
                Main.Instance.MapManager.AddItemOnScene(_itemOnStand, _pointForObject.position, Quaternion.identity);
                inventory.RemoveItemFromInventory(_itemOnStand);
                IsQuestCompleted = true;
                _checkAfterAddItem?.Invoke();
            }
        }
    }
}