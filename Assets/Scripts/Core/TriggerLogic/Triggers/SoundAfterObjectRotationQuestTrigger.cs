using Core.Quests;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.TriggerLogic.Triggers
{
    public class SoundAfterObjectRotationQuestTrigger : ITriggerListener
    {
        public bool IsTriggered => _isTriggered;

        private readonly JsonMessage<TriggerData> _message;
        private readonly TriggerSystem _system;

        private bool _isTriggered;

        public SoundAfterObjectRotationQuestTrigger(TriggerSystem system, JsonMessage<TriggerData> message)
        {
            _system = system;
            _message = message;
        }

        public void CheckTrigger()
        {
            if (_isTriggered)
            {
                return;
            }

            var quest = Main.Instance.QuestsStorage.GetQuestByType(QuestType.RotationObjects);
            if (quest == null)
            {
                return;
            }
            
            _isTriggered = quest.IsCompletedQuest();
        }

        public void ExecuteTrigger()
        {
            var ext = _message.GetExt<SoundExtensionData>();
            if (ext == null)
            {
                return;
            }

            Game.Instance.PlayerSound.PlayEffect(ext.Value.Clip);
        }

        public void TriggerCompleted()
        {
            _system.RemoveTrigger(this);
        }
    }
}