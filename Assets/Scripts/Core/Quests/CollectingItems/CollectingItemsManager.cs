using System.Collections.Generic;
using System.Linq;
using Core.Inventory.Item;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.Quests.CollectingItems
{
    public class CollectingItemsManager : QuestBase
    {
        [SerializeField] private List<ObjectStandQuest> _stands;
        [SerializeField] private AudioSource _audioTickTack;

        private readonly Dictionary<RequiredItemType, bool> _containItems = new Dictionary<RequiredItemType, bool>();

        public override QuestType Type => QuestType.CollectingItems;

        public override void Init(QuestManager manager, JsonMessage<QuestData> message)
        {
            base.Init(manager, message);
            _audioTickTack.enabled = false;

            var extension = message.GetExt<QuestCollectingItemsExceptionData>();
            
            if (extension == null)
            {
                Debug.LogError($"Unable to initialize the quest: {Type}");
                return;
            }

            foreach (var requiredItem in extension.Value.RequiredItems)
            {
                var stand = _stands.FirstOrDefault(stand => stand.RequiredItemType == requiredItem.Type);
                if (stand == null)
                {
                    Debug.LogWarning($"Stand not found: {requiredItem.Type}");
                    continue;
                }

                stand.Init(requiredItem.ItemId, CheckAfterAddItem);
            }
        }

        private void CheckAfterAddItem(RequiredItemType requiredType)
        {
            if (_containItems.ContainsKey(requiredType))
            {
                Debug.LogWarning($"The item already contains: {requiredType}");
                return;
            }
            _containItems[requiredType] = true;
            TryStartPlayClock();
            
            
            if (IsCompletedQuest())
            {
                Manager.QuestCompleted(Message.Data);
            }
        }

        private void TryStartPlayClock()
        {
            if (!_audioTickTack.isPlaying && _containItems.ContainsKey(RequiredItemType.Dial))
            {
                _audioTickTack.enabled = true;
                _audioTickTack.Play();
            }
        }
        
        public override bool IsCompletedQuest()
        {
            return _stands.All(obj => obj.IsQuestCompleted);
        }
    }
}