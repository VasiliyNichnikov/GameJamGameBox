using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.TriggerLogic.Triggers
{
    public class LightChangeTrigger : ITriggerListener
    {
        public bool ManyTimer => true;
        public bool IsTriggered { get; private set; }

        private bool _isNeededQuestCompleted;
        private readonly LightTriggerExtensionData _data;
        private readonly TimeBlocker _locker;


        public LightChangeTrigger(TriggerSystem system, JsonMessage<TriggerData> message)
        {
            var extension = message.GetExt<LightTriggerExtensionData>();
            if (extension == null)
            {
                Debug.LogError("Not initialized extension (LightTriggerExtensionData)");
                return;
            }

            _data = extension.Value;
            _locker = new TimeBlocker();
            Main.Instance.QuestManager.OnQuestCompleted += CheckCompletedQuest;
        }

        public void CheckTrigger()
        {
            IsTriggered = _isNeededQuestCompleted && !_locker.IsLock;
        }

        public void ExecuteTrigger()
        {
            var nearestLight = Main.Instance.MapManager.GetClosestLightToPlayer(_data.MinimumDistanceToPlayer);
            if (nearestLight == null)
            {
                return;
            }

            Debug.LogWarning("TODO: Need connect trigger system");
            StartLocker();
        }

        public void TriggerDestroy()
        {
        }

        private void CheckCompletedQuest(int questId)
        {
            if (_isNeededQuestCompleted)
            {
                return;
            }
            
            _isNeededQuestCompleted = _data.CompletedQuest == questId;
            if (_isNeededQuestCompleted && !_locker.IsLock)
            {
                StartLocker();
            }
        }

        private void StartLocker()
        {
            var randomRange = Random.Range(_data.MinimumDelaySeconds, _data.MaximumDelaySeconds);
            Debug.LogWarning($"Random locker time light: {randomRange}");
            _locker.SetTimeLock(randomRange).StartLock();
        }
        
        public void Dispose()
        {
            _locker.Dispose();
            Main.Instance.QuestManager.OnQuestCompleted -= CheckCompletedQuest;
        }
    }
}