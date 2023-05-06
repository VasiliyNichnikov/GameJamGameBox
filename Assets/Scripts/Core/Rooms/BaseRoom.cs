using Core.Doors;
using Core.Rooms.ManagementUtils;
using Core.SoundLogic.AtmosphericSounds;
using UnityEngine;

namespace Core.Rooms
{
    public enum RoomState
    {
        PlayerEnteredRoom,
        PlayerLeftRoom
    }
    
    public abstract class BaseRoom : MonoBehaviour
    {
        public abstract RoomType Type { get; }
        public abstract ISoundRoom SoundRoom { get; }
        public bool PlayerInRoom { get; private set; }

        [SerializeField] private EntryManager _entryManager;

        public void Init(PlayerObserver observer)
        {
            if (_entryManager != null)
            {
                _entryManager.Init(this);
            }
        }

        public void SetPlayerInRoom()
        {
            PlayerInRoom = true;
        }

        public void RemovePlayerInRoom()
        {
            PlayerInRoom = false;
        }
    }
}