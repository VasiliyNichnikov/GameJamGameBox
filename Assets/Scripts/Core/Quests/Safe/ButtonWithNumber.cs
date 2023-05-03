using System;
using TMPro;
using UnityEngine;

namespace Core.Quests.Safe
{
    public sealed class ButtonWithNumber : InteractionObjectBase
    {
        [SerializeField] private TextMeshPro _meshPro;
        public override bool IsDisplayedHintAfterInput => true;
        public override bool IsQuestCompleted { get; protected set; }

        private Action<int> _addNumberInResult;

        private int _number;
        
        public void Init(int number, Action<int> addNumberInResult)
        {
            _addNumberInResult = addNumberInResult;
            _meshPro.text = number.ToString();
            _number = number;
        }

        public void SetAsCompleted()
        {
            IsQuestCompleted = true;
        }
        
        public override void Input()
        {
            _addNumberInResult?.Invoke(_number);
        }
    }
}