using Core.Payer;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class HintCollectorView : DialogBase
    {
        [SerializeField] private Text _infoAboutAction;
        
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
            else if (!objectOnTrigger.IsItemEmpty() && !objectOnTrigger.Item!.CanTakeItem)
            {
                IsDisplayed = false;
            }
            else
            {
                IsDisplayed = !objectOnTrigger.IsEmpty();
            }

            if (IsDisplayed)
            {
                UpdateInfoAboutAction(objectOnTrigger);
                _selectedTrigger = objectOnTrigger;
            }
        }

        private void UpdateInfoAboutAction(ItemCollector.ObjectInTrigger objectOnTrigger)
        {
            if (objectOnTrigger.IsEmpty())
            {
                _infoAboutAction.gameObject.SetActive(false);
                return;
            }

            if (!objectOnTrigger.IsItemEmpty())
            {
                _infoAboutAction.gameObject.SetActive(true);
                _infoAboutAction.text = objectOnTrigger.Item!.InfoAboutItem;
                return;
            }

            if (!objectOnTrigger.IsInteractionEmpty() && objectOnTrigger.InteractionObject!.HasHint)
            {
                _infoAboutAction.gameObject.SetActive(true);
                _infoAboutAction.text = objectOnTrigger.InteractionObject!.Hint;
                return;
            }
        }
        
        public override void Dispose()
        {
            // nothing
        }
    }
}