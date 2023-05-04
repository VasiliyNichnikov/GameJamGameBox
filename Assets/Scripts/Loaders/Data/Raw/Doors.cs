using System.Collections.Generic;

namespace Loaders.Data.Raw
{
    public struct DoorRaw
    {
        public string Type { get; set; }
        public string RoomType { get; set; }
        public DoorKeyExtension? DoorKeyExtension;
    }

    public struct DoorsRow
    {
        public List<DoorRaw> Doors { get; set; }
    }
}