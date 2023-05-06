using System.Collections.Generic;
using System.Linq;
using Core.Doors;
using Core.SoundLogic;
using UnityEngine;

namespace Core.Rooms
{
    public class HouseSystem : MonoBehaviour
    {
        [SerializeField] private RoomType _startRoom;
        [SerializeField] private List<BaseRoom> _rooms;

        private PlayerObserver _playerObserver;
        
        public void Init(IBackgroundSoundManager soundManager)
        {
            _playerObserver = new PlayerObserver(soundManager, _rooms);
            foreach (var room in _rooms)
            {
                room.Init(_playerObserver);
            }
            
            var startRoom = _rooms.FirstOrDefault(room => room.Type == _startRoom);
            if (startRoom == null)
            {
                Debug.LogError($"StartRoom is null ({_startRoom}");
                return;
            }
            
            _playerObserver.SetStartRoom(startRoom);
        }

        private void OnDestroy()
        {
            _playerObserver.Dispose();
        }
    }
}