﻿using Core.Doors;
using Core.SoundLogic.AtmosphericSounds;
using UnityEngine;

namespace Core.Rooms
{
    public class Kitchen : BaseRoom
    {
        public override RoomType Type => RoomType.Kitchen;
        public override ISoundRoom SoundRoom => _kitchenSound;
        
        [SerializeField] private KitchenSound _kitchenSound;
    }
}