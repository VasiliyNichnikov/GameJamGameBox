using System;

namespace Core.Quests.FrontDoor
{
    public class FrontDoor : InteractionObjectBase
    {
        public override bool IsDisplayedHintAfterInput => false;
        public override bool IsQuestCompleted { get; protected set; }

        private Action _onStartPlot;
        
        public void Init(Action startPlot)
        {
            _onStartPlot = startPlot;
        }
        
        public override void Input()
        {
            if (IsQuestCompleted)
            {
                return;
            }

            _onStartPlot?.Invoke();
            IsQuestCompleted = true;
        }
    }
}