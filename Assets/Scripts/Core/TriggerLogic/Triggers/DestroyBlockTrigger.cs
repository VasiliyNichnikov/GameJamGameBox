using UnityEngine;

namespace Core.TriggerLogic.Triggers
{
    /// <summary>
    /// todo переделать это гавно, нужно перенести в json
    /// </summary>
    public class DestroyBlockTrigger : ITriggerListener
    {
        public bool ManyTimer => false;
        public bool IsTriggered { get; private set; }

        private bool _isNeededQuestCompleted;
        // todo нужно перенести в json
        private const int CompletedQuests = 5;
        
        public void CheckTrigger()
        {
            IsTriggered = _isNeededQuestCompleted;
        }

        public DestroyBlockTrigger()
        {
            Main.Instance.QuestManager.OnQuestCompleted += CheckCompetedQuest;
        }
        
        public void ExecuteTrigger()
        {
            Main.Instance.MapManager.DestroyBlockTrigger();
        }

        public void TriggerDestroy()
        {
        }

        private void CheckCompetedQuest(int questId)
        {
            if (_isNeededQuestCompleted)
            {
                return;
            }
            
            _isNeededQuestCompleted = questId == CompletedQuests;
        }
        
        public void Dispose()
        {
            Main.Instance.QuestManager.OnQuestCompleted -= CheckCompetedQuest;
        }
    }
}