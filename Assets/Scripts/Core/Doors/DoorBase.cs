using Core.Quests;
using DG.Tweening;
using UnityEngine;

namespace Core.Doors
{
    public abstract class DoorBase : InteractionObjectBase
    {
        public RoomType GetRoomType() => RoomType;
        
        [SerializeField] private Transform _pointRotation;
        [SerializeField] protected RoomType RoomType;
        [SerializeField] private float _angleOpen;
        [SerializeField] private float _angleClose;
        [SerializeField] private float _duration;


        private bool _isAnimation;
        private bool _isOpened;
        
        public void Open()
        {
            if (_isAnimation)
            {
                return;
            }

            _isOpened = true;
            RotateDoor(_angleOpen);
        }

        public void Close()
        {
            if (_isAnimation)
            {
                return;
            }

            _isOpened = false;
            RotateDoor(_angleClose);
        }

        protected void ChangeStateDoor()
        {
            if (_isOpened)
            {
                Close();
                return;
            }
            Open();
        }

        private void RotateDoor(float angle)
        {
            _isAnimation = true;
            var rotation = new Vector3(0, angle, 0);
            _pointRotation.DORotate(rotation, _duration).OnComplete(() => _isAnimation = false);
        }
    }
}