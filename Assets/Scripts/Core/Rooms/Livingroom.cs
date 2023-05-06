using Core.Doors;
using Core.SoundLogic.AtmosphericSounds;
using UnityEngine;

namespace Core.Rooms
{
    public class Livingroom : BaseRoom
    {
        public override RoomType Type => RoomType.Livingroom;
        public override ISoundRoom SoundRoom => _livingRoomSound;

        [SerializeField] private LivingRoomSound _livingRoomSound;
    }
}