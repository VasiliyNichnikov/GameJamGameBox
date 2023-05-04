namespace Core.Doors
{
    public class DoorQuest : DoorBase
    {
        public override bool IsDisplayedHintAfterInput => false;
        public override bool IsQuestCompleted { get; protected set; } = true;

        public override void Input()
        {
            // nothing
        }
    }
}