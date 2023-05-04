using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.Quests.FrontDoor
{
    public class FrontDoorManager : QuestBase
    {
        [SerializeField] private FrontDoor _frontDoor;
        private bool _isQuestCompleted;
        
        public override QuestType Type => QuestType.FrontDoor;

        public override void Init(QuestManager manager, JsonMessage<QuestData> message)
        {
            base.Init(manager, message);

            _frontDoor.Init(StartPlot);
        }

        public override bool IsCompletedQuest()
        {
            return _isQuestCompleted;
        }
    }
}