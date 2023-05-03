using Core.Map;
using Loaders;
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

        public static JsonMessage<T> GetMessageForTextDialogExt<T>(T data, string namePerson, string messagePerson)
            where T : struct
        {
            var extension = new TextDialogExtensionData(namePerson, messagePerson);
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
    }
}