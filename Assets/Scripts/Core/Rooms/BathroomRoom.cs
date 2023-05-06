using Core.Doors;
using Core.SoundLogic.AtmosphericSounds;
using UnityEngine;

namespace Core.Rooms
{
    public class BathroomRoom : BaseRoom
    {
        public override RoomType Type => RoomType.Bathroom;
        public override ISoundRoom SoundRoom => null;
    }
}