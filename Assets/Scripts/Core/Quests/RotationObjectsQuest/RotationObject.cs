using System;
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

        private Quaternion _rightRotation;
        private int _numberOfTurns;
        private Action _onAfterTurnAction;


        public bool IsRightTurn {
            get
            {
                switch (_checkAxis)
                {
                    case CheckAxis.X:
                        return Math.Abs(_selectedObject.localRotation.x - _rightRotation.x) < 0.01f;
                    case CheckAxis.Y:
                        return Math.Abs(_selectedObject.localRotation.y - _rightRotation.y) < 0.01f;
                    case CheckAxis.Z:
                        return Math.Abs(_selectedObject.localRotation.z - _rightRotation.z) < 0.01f;
                }

                return false;
            }
        }

        private void Awake()
        {
            _rightRotation = Quaternion.Euler(_rotateAfterClick * _numberRotation);
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
            _numberOfTurns++;
            // плохой способ, но другим не смог решить
            _selectedObject.localRotation = Quaternion.Euler(_rotateAfterClick * _numberOfTurns);
        }
    }
}