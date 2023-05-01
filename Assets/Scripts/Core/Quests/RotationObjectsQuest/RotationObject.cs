using System;
using UnityEngine;

namespace Core.Quests.RotationObjectsQuest
{
    public class RotationObject : InteractionObjectBase
    {
        [SerializeField] private Transform _selectedObject;
        [SerializeField] private Vector3 _rotateAfterClick;

        [SerializeField, Range(1, 100)] private int _numberRotation;

        private Quaternion _rightRotation;

        private int _numberOfTurns;

        public bool IsRightTurn => _selectedObject.rotation == _rightRotation;

        private void Awake()
        {
            _rightRotation = Quaternion.Euler(_rotateAfterClick * _numberRotation);
        }
        
        public override bool IsDisplayedHintAfterInput => true;

        private Action _onRotate;

        // Написано для теста
        public void Init(Action onRotate)
        {
            _onRotate = onRotate;
        }

        public override void Input()
        {
            Turn();
        }

        private void Turn()
        {
            _numberOfTurns++;
            // плохой способ, но другим не смог решить
            _selectedObject.rotation = Quaternion.Euler(_rotateAfterClick * _numberOfTurns);
            _onRotate?.Invoke();
        }
    }
}