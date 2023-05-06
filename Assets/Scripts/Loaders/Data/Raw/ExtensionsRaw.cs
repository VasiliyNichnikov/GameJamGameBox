using System.Collections.Generic;

namespace Loaders.Data.Raw
{
    public struct SoundExtension
    {
        public string Sound { get; set; }
    }

    public struct TextDialogExtension
    {
        public string NamePerson { get; set; }
        public string MessagePerson { get; set; }
        public bool? ShowExitButton { get; set; }
    }

    public struct TimerExtension
    {
        public float Timer { get; set; }
    }

    public struct ChangeStateObjectExtension
    {
        public string NameObject { get; set; }
        public bool State { get; set; }
    }

    public struct OpenDoorExtension
    {
        public string NameDoor { get; set; }
        public string NameAnimation { get; set; }
    }

    public struct QuestSafeExtension
    {
        public int StartValue { get; set; }
        public int RightAnswer { get; set; }
        public int ItemInside { get; set; }
        public List<float> ScaleItem { get; set; }
        public List<float> RotationItem { get; set; }
    }

    public struct DoorKeyExtension
    {
        public int NeededItem { get; set; }
        public string Hint { get; set; }
    }

    public struct CreateItemExtension
    {
        public int ItemId { get; set; }
        public List<float> Position { get; set; }
        public List<float> Rotation { get; set; }
        public List<float> Scale { get; set; }
    }

    public struct QuestCollectingItemsException
    {
        public struct RequiredItem
        {
            public string Type { get; set; }
            public int ItemId { get; set; }
        }
        
        public List<RequiredItem> RequiredItems { get; set; }
    }

    public struct LightTriggerExtension
    {
        public int CompletedQuest { get; set; }
        public float MinimumDelaySeconds { get; set; }
        public float MaximumDelaySeconds { get; set; }
        public float MinimumDistanceToPlayer { get; set; }
    }
}