namespace Core.Doors
{
    public class DoorOpen : DoorBase
    {
        public override bool IsDisplayedHintAfterInput => true;
        public override bool IsQuestCompleted { get; protected set; } = false;

        public override void Input()
        {
            ChangeStateDoor();
        }
    }
}