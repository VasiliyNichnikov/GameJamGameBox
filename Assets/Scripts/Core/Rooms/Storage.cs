using Core.Doors;
using Core.SoundLogic.AtmosphericSounds;

namespace Core.Rooms
{
    public class Storage : BaseRoom
    {
        public override RoomType Type => RoomType.Storage;
        public override ISoundRoom SoundRoom => null;
    }
}