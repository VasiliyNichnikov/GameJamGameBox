using UnityEngine;

namespace Loaders.Data.Ready
{
    public struct SoundExtensionData
    {
        public readonly AudioClip Clip;

        public SoundExtensionData(AudioClip clip)
        {
            Clip = clip;
        }
    }

    public struct TextDialogExtensionData
    {
        public readonly string NamePerson;
        public readonly string MessagePerson;

        public TextDialogExtensionData(string namePerson, string messagePerson)
        {
            NamePerson = namePerson;
            MessagePerson = messagePerson;
        }
    }

    public struct TimerExtensionData
    {
        public readonly float Time;

        public TimerExtensionData(float time)
        {
            Time = time;
        }
    }

    public struct ChangeStateObjectExtensionData
    {
        public readonly string ObjectForChanges;
        public readonly bool State;

        public ChangeStateObjectExtensionData(string objectForChanges, bool state)
        {
            ObjectForChanges = objectForChanges;
            State = state;
        }
    }

    public struct OpenDoorExtensionData
    {
        public readonly string NameDoor;

        public OpenDoorExtensionData(string nameDoor, string nameAnimation)
        {
            NameDoor = nameDoor;
        }
    }

    public struct QuestSafeExtensionData
    {
        public readonly int StartValue;
        public readonly int RightAnswer;
        public readonly int ItemInside;
        public readonly Vector3 ScaleItem;
        public readonly Quaternion RotationItem;

        public QuestSafeExtensionData(int startValue, int rightAnswer, int itemInside, Vector3 scaleItem, Quaternion rotationItem)
        {
            StartValue = startValue;
            RightAnswer = rightAnswer;
            ItemInside = itemInside;
            ScaleItem = scaleItem;
            RotationItem = rotationItem;
        }
    }

    public struct DoorKeyExtensionData
    {
        public readonly int NeededItem;
        public readonly string Hint;

        public DoorKeyExtensionData(int neededItem, string hint)
        {
            NeededItem = neededItem;
            Hint = hint;
        }
    }
}