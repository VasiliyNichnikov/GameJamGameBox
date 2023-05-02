using System;
using Core.Payer.Movement;
using UnityEngine;

namespace Core.Payer
{
    public class PlayerAnimationCamera : MonoBehaviour
    {
        [SerializeField] private float[] _positionsAnimationAxesZ;
        [SerializeField][Range(0, 0.2f)] private float _step;
        private float _progress;
        private int _idAnimation;
        private int _endAnimation;
    
        private float _p0;
        private float _p1;
    
        private void Start()
        {
            _idAnimation = 0;
            _progress = 0;
            _endAnimation = _positionsAnimationAxesZ.Length - 2;

            SelectP0AndP1();
        }

        public float GetRotationZ()
        {
            float z = GetValueAnimation(_progress);
            if(ControlWalk.IsMovement)
            {
                _progress += _step;

                if (_progress >= 1)
                {
                    NextFrame();
                }
            }
        
            return z;
        }
    
        private float GetValueAnimation(float t)
        {
            if (t < 0 || t > 1)
            {
                throw new Exception("t выходит за допустимый диапазон");
            }

            return Mathf.Lerp(_p0, _p1, t);
        }

        private void NextFrame()
        {
            _idAnimation++;
            _progress = 0;
            if (_idAnimation > _endAnimation)
            {
                _idAnimation = 0;
            }

            SelectP0AndP1();
        }

        private void SelectP0AndP1()
        {
            _p0 = _positionsAnimationAxesZ[_idAnimation];
            _p1 = _positionsAnimationAxesZ[_idAnimation + 1];
        }
    
    }
}
