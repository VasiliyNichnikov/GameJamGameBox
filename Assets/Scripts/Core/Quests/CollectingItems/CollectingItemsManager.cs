using System.Collections.Generic;
using System.Linq;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.Quests.CollectingItems
{
    public class CollectingItemsManager : QuestBase
    {
        [SerializeField] private List<ObjectStandQuest> _stands;

        public override QuestType Type => QuestType.CollectingItems;

        public override void Init(QuestManager manager, JsonMessage<QuestData> message)
        {
            base.Init(manager, message);

            if (message.Data.RequiredItems == null || 
                message.Data.RequiredItems.Count == 0 ||
                _stands.Count != message.Data.RequiredItems.Count)
            {
                Debug.LogError($"Unable to initialize the quest: {Type}");
                return;
            }

            for (var i = 0; i < _stands.Count; i++)
            {
                var item = message.Data.RequiredItems[i];
                var stand = _stands[i];
                stand.Init(item.ObjectType, CheckAfterAddItem);
            }
        }

        private void CheckAfterAddItem()
        {
            if (IsCompletedQuest())
            {
                Manager.QuestCompleted(Message.Data);
            }
        }

        public override bool IsCompletedQuest()
        {
            return _stands.All(obj => obj.IsQuestCompleted);
        }
    }
}