using Core.Doors;
using Extensions;
using Loaders.Data.Raw;

namespace Loaders.Data.Ready
{
    public struct DoorData
    {
        public readonly DoorType DoorType;
        public readonly RoomType RoomType;

        public DoorData(DoorRaw data)
        {
            DoorType = data.Type.ConvertToEnum<DoorType>();
            RoomType = data.RoomType.ConvertToEnum<RoomType>();
        }
    }
}