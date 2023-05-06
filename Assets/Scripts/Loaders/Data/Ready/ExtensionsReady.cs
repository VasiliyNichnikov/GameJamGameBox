using System.Collections.Generic;
using Core.Quests.CollectingItems;
using Extensions;
using Loaders.Data.Raw;
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
        public readonly bool ShowExitButton;

        public TextDialogExtensionData(string namePerson, string messagePerson, bool showExitButton)
        {
            NamePerson = namePerson;
            MessagePerson = messagePerson;
            ShowExitButton = showExitButton;
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

    public struct CreateItemExtensionData
    {
        public readonly int ItemId;
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        public readonly Vector3 Scale;

        public CreateItemExtensionData(CreateItemExtension extensionRaw)
        {
            ItemId = extensionRaw.ItemId;
            Position = extensionRaw.Position.ConvertToVector3();
            Rotation = extensionRaw.Rotation.ConvertToRotation();
            Scale = extensionRaw.Scale.ConvertToVector3();
        }
    }

    public struct QuestCollectingItemsExceptionData
    {
        public readonly List<RequiredItemData> RequiredItems;

        public QuestCollectingItemsExceptionData(QuestCollectingItemsException exceptionRaw)
        {
            RequiredItems = new List<RequiredItemData>();
            foreach (var item in exceptionRaw.RequiredItems)
            {
                RequiredItems.Add(new RequiredItemData(item));
            }
        }
    }
    
    public struct RequiredItemData
    {
        public readonly int ItemId;
        public readonly RequiredItemType Type;

        public RequiredItemData(QuestCollectingItemsException.RequiredItem requiredItemRaw)
        {
            ItemId = requiredItemRaw.ItemId;
            Type = requiredItemRaw.Type.ConvertToEnum<RequiredItemType>();
        }
    }

    public struct LightTriggerExtensionData
    {
        public readonly int CompletedQuest;
        public readonly float MinimumDelaySeconds;
        public readonly float MaximumDelaySeconds;
        public readonly float MinimumDistanceToPlayer;

        public LightTriggerExtensionData(LightTriggerExtension extensionRaw)
        {
            CompletedQuest = extensionRaw.CompletedQuest;
            MinimumDelaySeconds = extensionRaw.MinimumDelaySeconds;
            MaximumDelaySeconds = extensionRaw.MaximumDelaySeconds;
            MinimumDistanceToPlayer = extensionRaw.MinimumDistanceToPlayer;
        }
    }
}