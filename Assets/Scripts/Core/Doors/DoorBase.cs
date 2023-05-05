using Core.Quests;
using DG.Tweening;
using UnityEngine;

namespace Core.Doors
{
    public abstract class DoorBase : InteractionObjectBase
    {
        public RoomType GetRoomType() => RoomType;

        private enum AxisRotation
        {
            X,
            Y,
            Z
        }

        [SerializeField] private AxisRotation _selectedAxis;
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
            var currentRotation = _pointRotation.rotation.eulerAngles;
            var rotation = Quaternion.identity;

            switch (_selectedAxis)
            {
                case AxisRotation.X:
                    rotation = Quaternion.Euler(angle, currentRotation.y, currentRotation.z);
                    break;
                case AxisRotation.Y:
                    rotation = Quaternion.Euler(currentRotation.x, angle, currentRotation.z);
                    break;

                case AxisRotation.Z:
                    rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, angle);
                    break;
            }

            _isAnimation = true;
            _pointRotation.DORotateQuaternion(rotation, _duration).OnComplete(() => _isAnimation = false);
        }
    }
}