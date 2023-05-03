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
}