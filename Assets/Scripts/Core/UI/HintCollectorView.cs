using Core.Payer;
using UnityEngine;

namespace Core.UI
{
    public class HintCollectorView : DialogBase
    {
        private bool IsDisplayed
        {
            get => _isDisplayed;
            set
            {
                if (value != _isDisplayed)
                {
                    _isDisplayed = value;
                    gameObject.SetActive(_isDisplayed);
                }
            }
        }

        private ItemCollector.ObjectInTrigger _selectedTrigger;
        private bool _isDisplayed;

        private void Start()
        {
            gameObject.SetActive(false);
            ItemCollector.OnChangeStateActive += ChangeStateHint;
        }

        private void OnDestroy()
        {
            ItemCollector.OnChangeStateActive -= ChangeStateHint;
        }

        private void Update()
        {
            if (!IsDisplayed)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_selectedTrigger.IsItemEmpty())
                {
                    _selectedTrigger.Item!.ToTake();
                    IsDisplayed = false;
                    return;
                }

                if (!_selectedTrigger.IsInteractionEmpty())
                {
                    _selectedTrigger.InteractionObject!.Input();
                    var interactionObject = _selectedTrigger.InteractionObject!;
                    IsDisplayed = interactionObject.IsDisplayedHintAfterInput && !interactionObject.IsQuestCompleted;
                    return;
                }
            }
        }

        private void ChangeStateHint(ItemCollector.ObjectInTrigger objectOnTrigger)
        {
            // Небольшой костыль. Мы не будем показывать букву E, если квест выполнен
            if (!objectOnTrigger.IsInteractionEmpty() && objectOnTrigger.InteractionObject!.IsQuestCompleted)
            {
                IsDisplayed = false;
            }
            else
            {
                IsDisplayed = !objectOnTrigger.IsEmpty();
            }

            if (IsDisplayed)
            {
                _selectedTrigger = objectOnTrigger;
            }
        }

        public override void Dispose()
        {
            // nothing
        }
    }
}