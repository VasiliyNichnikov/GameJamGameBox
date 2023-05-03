using System.Collections.Generic;
using DG.Tweening;
using Loaders.Data.Ready;
using TMPro;
using UnityEngine;
using Utils;

namespace Core.Quests.Safe
{
    public class SafeManager : QuestBase
    {
        private struct ItemData
        {
            public int ItemInsideId;
            public Vector3 Scale;
            public Quaternion Rotation;
        }
        
        [SerializeField] private TextMeshPro _resultText;
        [SerializeField] private RectTransform _textResultTransform;
        [SerializeField] private List<ButtonWithNumber> _buttons;
        
        [Space][SerializeField] private Transform _pointForItemInside;
        
        [Space]
        [SerializeField, Header("Встряска по осям")] private Vector3 _strength;
        [SerializeField] private float _timeAnimation;
        
        public override QuestType Type => QuestType.Safe;

        private int _currentResult;
        private int _resultValue;
        private int _startValue;
        private bool _isCompleted;
        private ItemData _itemInside;
        
        public override void Init(QuestManager manager, JsonMessage<QuestData> message)
        {
            base.Init(manager, message);

            var extension = message.GetExt<QuestSafeExtensionData>();
            if (extension == null)
            {
                Debug.LogError($"Not corrected message (QuestSafeExtensionData)");
                return;
            }

            _resultValue = extension.Value.RightAnswer;
            _startValue = extension.Value.StartValue;
            _currentResult = _startValue;
            _itemInside = new ItemData
            {
                ItemInsideId = extension.Value.ItemInside,
                Rotation = extension.Value.RotationItem,
                Scale = extension.Value.ScaleItem
            };
                
                
            if (_buttons.Count != 9)
            {
                Debug.LogError("Number buttons must be equal 9");
                return;
            }

            UpdateResultText();
            for (var i = 0; i < 9; i++)
            {
                var button = _buttons[i];
                var number = i + 1;
                button.Init(number, AddNumberInResult);
            }
        }

        private void AddNumberInResult(int number)
        {
            if (_isCompleted)
            {
                EndQuest();
                return;
            }
            
            _currentResult += number;
            UpdateResultText();
        }

        private void UpdateResultText()
        {
            _resultText.text = $"{_currentResult}/{_resultValue}";
            _isCompleted = _currentResult == _resultValue;

            if (_currentResult > _resultValue)
            {
                _textResultTransform.DOShakePosition(_timeAnimation, _strength, randomness:10, randomnessMode:ShakeRandomnessMode.Harmonic).
                    OnComplete(() =>
                    {
                        _currentResult = _startValue;
                        UpdateResultText();
                    });
            }

            if (_isCompleted)
            {
                EndQuest();
            }
        }

        private void EndQuest()
        {
            foreach (var button in _buttons)
            {
                button.SetAsCompleted();
            }

            var item = Main.Instance.MapManager.AddItemOnScene(
                _itemInside.ItemInsideId, 
                Vector3.zero,
                _itemInside.Rotation);
            item.transform.rotation = _itemInside.Rotation;
            item.transform.localScale = _itemInside.Scale;
            item.transform.SetParent(_pointForItemInside, false);
            
            Manager.QuestCompleted(Message.Data);
        }
        
        public override bool IsCompletedQuest()
        {
            return _isCompleted;
        }
    }
}