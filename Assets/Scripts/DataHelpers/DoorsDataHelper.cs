using System.Collections.Generic;
using Loaders;
using Loaders.Data.Raw;
using Loaders.Data.Ready;
using Utils;

namespace DataHelpers
{
    public class DoorsDataHelper
    {
        public IReadOnlyCollection<JsonMessage<DoorData>> Doors => _doors;

        private readonly List<JsonMessage<DoorData>> _doors = new List<JsonMessage<DoorData>>();

        private const string DoorFile = "Doors";
        
        public void Load()
        {
            var data = StaticLoader.LoadData<DoorsRow>(DoorFile);
            foreach (var door in data.Doors)
            {
                var doorData = new DoorData(door);
                var jsonMessage = door.DoorKeyExtension == null
                    ? new JsonMessage<DoorData>(doorData, null)
                    : ExtensionHelper.GetMessageForDoorKey(doorData, door.DoorKeyExtension.Value);

                _doors.Add(jsonMessage);
            }
        }
    }
}