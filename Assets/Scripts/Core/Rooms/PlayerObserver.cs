using System;
using System.Collections.Generic;
using System.Linq;
using Core.Rooms.ManagementUtils;
using Core.SoundLogic;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Rooms
{
    /// <summary>
    /// Проверяет в какой комнате сейчас игрок и
    /// содержит логику наблюдения за игроком
    /// </summary>
    public class PlayerObserver : IDisposable
    {
        private readonly IBackgroundSoundManager _soundManager;
        private readonly List<BaseRoom> _rooms;

        /// <summary>
        /// Предыдующая комната где был игрок
        /// </summary>
        private BaseRoom _previewRoom;

        /// <summary>
        /// Текущая комната где находится игрок
        /// </summary>
        [CanBeNull] private BaseRoom _currentRoom;

        public PlayerObserver(IBackgroundSoundManager soundManager, IEnumerable<BaseRoom> rooms)
        {
            _soundManager = soundManager;
            _rooms = rooms.ToList();

            EntryManager.OnUpdateTrigger += ChangeRoomState;
        }

        public void SetStartRoom(BaseRoom room)
        {
            ChangeRoomState(room, RoomState.PlayerEnteredRoom);
        }
        

        private void ChangeRoomState(BaseRoom room, RoomState state)
        {
            switch (state)
            {
                case RoomState.PlayerEnteredRoom:
                    PlayerEnteredInRoom(room);
                    break;
                case RoomState.PlayerLeftRoom:
                    PlayerLeftRoom();
                    break;
            }
        }

        private void PlayerEnteredInRoom(BaseRoom room)
        {
            Debug.Log($"Player entered room: {room.Type}");
            _currentRoom = room;
            room.SetPlayerInRoom();
            _soundManager.EntranceToRoom(room.SoundRoom);
        }

        private void PlayerLeftRoom()
        {
            _previewRoom = _currentRoom;
            if (_previewRoom == null)
            {
                Debug.LogWarning("PreviewRoom is null");
                return;
            }
            
            Debug.Log($"Player left room: {_previewRoom.Type}");
            _previewRoom.RemovePlayerInRoom();
            _soundManager.ExitFromRoomToHall(_previewRoom.SoundRoom);
        }

        public void Dispose()
        {
            EntryManager.OnUpdateTrigger -= ChangeRoomState;
        }
    }
}