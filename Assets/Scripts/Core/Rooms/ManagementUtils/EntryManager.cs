using System;
using UnityEngine;

namespace Core.Rooms.ManagementUtils
{
    public class EntryManager : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;

        private const string PlayerTag = "Player";


        public static event Action<BaseRoom, RoomState> OnUpdateTrigger;

        private BaseRoom _room;

        public void Init(BaseRoom room)
        {
            _room = room;

            name = $"EntryManager_{_room.Type}";
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(PlayerTag))
            {
                return;
            }

            ChangeStateRoom(RoomState.PlayerEnteredRoom);
        }
        
        public void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(PlayerTag))
            {
                return;
            }

            ChangeStateRoom(RoomState.PlayerLeftRoom);
        }

        private void ChangeStateRoom(RoomState state)
        {
            Debug.Log($"CurrentRoomState: {state}. Name: {name}");
            OnUpdateTrigger?.Invoke(_room, state);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_collider == null)
            {
                return;
            }

            Gizmos.color = new Color(0, 1, 0, 0.35f);
            var sizeEntryManager = transform.lossyScale;
            var sizeCollider = _collider.size;
            var center = transform.position + _collider.center;
            var size = new Vector3(sizeEntryManager.x * sizeCollider.x, 
                sizeEntryManager.y * sizeCollider.y, 
                sizeEntryManager.z * sizeCollider.z);

            Gizmos.DrawCube(center, size);
        }
#endif
    }
}