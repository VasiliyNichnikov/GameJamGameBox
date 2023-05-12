using Extensions;
using Loaders;
using Loaders.Data.Raw;
using Loaders.Data.Ready;

namespace Utils
{
    public static class ExtensionHelper
    {
        public static JsonMessage<T> GetMessageForSoundExt<T>(T data, string nameFile) where T : struct
        {
            var clip = StaticLoader.LoadData(nameFile);
            var extension = new SoundExtensionData(clip);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForTextDialogExt<T>(T data, string namePerson, string messagePerson, bool? showExitButton)
            where T : struct
        {
            var result = showExitButton ?? false;
            var extension = new TextDialogExtensionData(namePerson, messagePerson, result);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForTimer<T>(T data, float time) where T : struct
        {
            var extension = new TimerExtensionData(time);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForChangeStateObject<T>(T data, string objectForChanges,
            bool state) where T : struct
        {
            var extension = new ChangeStateObjectExtensionData(objectForChanges, state);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForOpenDoor<T>(T data, string nameDoor, string nameAnimation)
            where T : struct
        {
            var extension = new OpenDoorExtensionData(nameDoor, nameAnimation);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForQuestSafe<T>(T data, QuestSafeExtension extensionRow) where T: struct
        {
            var extension = new QuestSafeExtensionData(extensionRow.StartValue, 
                extensionRow.RightAnswer, 
                extensionRow.ItemInside, 
                extensionRow.ScaleItem.ConvertToVector3(),
                extensionRow.RotationItem.ConvertToRotation());
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForDoorKey<T>(T data, DoorKeyExtension extensionRow) where T : struct
        {
            var extension = new DoorKeyExtensionData(extensionRow.NeededItem, extensionRow.Hint);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForCreateItem<T>(T data, CreateItemExtension extensionRaw)
            where T : struct
        {
            var extension = new CreateItemExtensionData(extensionRaw);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForCollectingItems<T>(T data, QuestCollectingItemsException exceptionRaw) where T: struct
        {
            var extension = new QuestCollectingItemsExceptionData(exceptionRaw);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForLightTrigger<T>(T data, LightTriggerExtension extensionRaw) where T: struct
        {
            var extension = new LightTriggerExtensionData(extensionRaw);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }

        public static JsonMessage<T> GetMessageForActionMonster<T>(T data, ActionMonsterExtension extensionRaw)
            where T : struct
        {
            var extension = new ActionMonsterExtensionData(extensionRaw);
            var jsonMessage = new JsonMessage<T>(data, extension);
            return jsonMessage;
        }
    }
}