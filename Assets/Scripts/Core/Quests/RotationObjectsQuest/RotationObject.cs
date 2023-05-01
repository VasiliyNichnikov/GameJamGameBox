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
        private Action _onAfterTurnAction;

        public bool IsRightTurn => _selectedObject.rotation == _rightRotation;

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
            _selectedObject.rotation = Quaternion.Euler(_rotateAfterClick * _numberOfTurns);
        }
    }
}