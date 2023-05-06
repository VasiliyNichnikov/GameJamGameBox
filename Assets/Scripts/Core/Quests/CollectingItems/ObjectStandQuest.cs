using System;
using Core.Inventory.Item;
using UnityEngine;

namespace Core.Quests.CollectingItems
{
    public class ObjectStandQuest : InteractionObjectBase
    {
        public RequiredItemType RequiredItemType => Type;
        
        [SerializeField, Header("Объект, который нужно показать после получения предмета")] private GameObject _standItem;
        [SerializeField] private RequiredItemType Type;

        public override bool IsDisplayedHintAfterInput => true;
        public override bool IsQuestCompleted { get; protected set; }

        public override bool HasHint => true;
        public override string Hint {
            get
            {
                switch (RequiredItemType)
                {
                    case RequiredItemType.Dial:
                        return "Где-то лежит стрелка!";
                    case RequiredItemType.Pendulum:
                        return "Тут должен быть маятник";
                    case RequiredItemType.Bird:
                        return "Домик для птички";
                }

                return string.Empty;
            }
        }


        private Action<RequiredItemType> _checkAfterAddItem;
        private int _neededItemId; 
        
        public void Init(int needItem, Action<RequiredItemType> checkAfterAddItem)
        {
            _neededItemId = needItem;
            _standItem.SetActive(false);
            _checkAfterAddItem = checkAfterAddItem;
        }
        
        public override void Input()
        {
            if (IsQuestCompleted)
            {
                return;
            }
            
            var inventory = Game.Instance.InventoryManager;
            if (inventory.IsThereItem(_neededItemId))
            {
                // Порядок важен
                _standItem.SetActive(true);
                inventory.RemoveItemFromInventory(_neededItemId);
                IsQuestCompleted = true;
                _checkAfterAddItem?.Invoke(Type);
                enabled = false;
            }
        }
    }
}