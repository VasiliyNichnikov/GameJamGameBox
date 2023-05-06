using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Doors
{
    /// <summary>
    /// todo работает нормально только с дверями Key
    /// Так как когда в одной комнате дверей одного типа > 1, вылетает ошибка
    /// </summary>
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