using Core.Payer;
using Core.Quests;
using DG.Tweening;
using UnityEngine;

namespace Core.Doors
{
    public abstract class DoorBase : InteractionObjectBase, INoisyObject
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

        private const float NoisyVolume = 10f;

        private bool _isAnimation;
        private bool _isOpened;
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPlayer">Дверь была открыта игроком</param>
        public void Open(bool isPlayer)
        {
            if (_isAnimation)
            {
                return;
            }

            if (isPlayer)
            {
                MakeSound();
            }

            _isOpened = true;
            RotateDoor(_angleOpen);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPlayer">Дверь была закрыта игроком</param>
        public void Close(bool isPlayer)
        {
            if (_isAnimation)
            {
                return;
            }
            
            if (isPlayer)
            {
                MakeSound();
            }
            
            _isOpened = false;
            RotateDoor(_angleClose);
        }

        protected void ChangeStateDoor(bool isPlaying)
        {
            if (_isOpened)
            {
                Close(isPlaying);
                return;
            }

            Open(isPlaying);
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

        public void MakeSound(float noiseVolume = NoisyVolume)
        {
            Game.Instance.PlayerSound.MakeSound(noiseVolume);
        }
    }
}