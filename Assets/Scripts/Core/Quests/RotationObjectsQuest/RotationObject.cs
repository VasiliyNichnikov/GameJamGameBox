using System;
using DG.Tweening;
using UnityEngine;

namespace Core.Quests.RotationObjectsQuest
{
    public class RotationObject : InteractionObjectBase
    {
        private enum CheckAxis
        {
            X, 
            Y, 
            Z
        }

        [SerializeField] private Transform _selectedObject;
        [SerializeField] private Vector3 _rotateAfterClick;

        [SerializeField, Range(1, 100)] private int _numberRotation;
        [SerializeField] private CheckAxis _checkAxis;

        private float _rightAngle;
        private int _numberOfTurns;
        private Action _onAfterTurnAction;


        public bool IsRightTurn {
            get
            {
                var currentAngle = GetAngle(_selectedObject.localRotation.eulerAngles);
                if (currentAngle > 180)
                {
                    currentAngle = 360 - currentAngle;
                }
                return Math.Abs(_rightAngle - currentAngle) < 0.01f;
            }
        }

        private void Awake()
        {
            _rightAngle = GetAngle(_rotateAfterClick) * _numberRotation;
        }
        
        public override bool IsDisplayedHintAfterInput => true;
        public override bool IsQuestCompleted { get; protected set; }


        public void Init(Action afterTurnAction)
        {
            _onAfterTurnAction = afterTurnAction;
        }
        
        
        public override void Input()
        {
            Turn();
            _onAfterTurnAction?.Invoke();
        }

        public void CompleteQuest()
        {
            IsQuestCompleted = true;
        }

        private void Turn()
        {
            _selectedObject.Rotate(_rotateAfterClick);
        }

        private float GetAngle(Vector3 objectForRotation)
        {
            switch (_checkAxis)
            {
                case CheckAxis.X:
                    return objectForRotation.x;
                case CheckAxis.Y:
                    return objectForRotation.y;
                case CheckAxis.Z:
                    return objectForRotation.z;
            }

            Debug.LogError($"Not corrected axis ({_checkAxis}");
            return 0.0f;
        }
    }
}