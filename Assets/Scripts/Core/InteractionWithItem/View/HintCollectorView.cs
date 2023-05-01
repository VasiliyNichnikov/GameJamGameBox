using Core.Inventory.Item;
using Core.Payer;
using Core.Quests;
using UnityEngine;

namespace Core.InteractionWithItem.View
{
    public class HintCollectorView : MonoBehaviour
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
                    IsDisplayed = _selectedTrigger.InteractionObject!.IsDisplayedHintAfterInput;
                    return;
                }
            }
        }

        private void ChangeStateHint(ItemCollector.ObjectInTrigger objectOnTrigger)
        {
            IsDisplayed = !objectOnTrigger.IsEmpty();
            
            
            if (IsDisplayed)
            {
                _selectedTrigger = objectOnTrigger;
            }
        }
    }
}