using Core.Doors;
using Core.SoundLogic.AtmosphericSounds;
using UnityEngine;

namespace Core.Rooms
{
    public class BedRoom : BaseRoom
    {
        public override RoomType Type => RoomType.Bedroom;
        public override ISoundRoom SoundRoom => null;
    }
}