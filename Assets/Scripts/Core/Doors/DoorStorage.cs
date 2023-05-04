using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Doors
{
    public class DoorStorage: MonoBehaviour
    {
        [SerializeField] private List<DoorBase> _doors;

        [CanBeNull]
        public DoorBase GetDoorByType(RoomType type)
        {
            return _doors.FirstOrDefault(room => room.GetRoomType() == type);
        }
    }
}