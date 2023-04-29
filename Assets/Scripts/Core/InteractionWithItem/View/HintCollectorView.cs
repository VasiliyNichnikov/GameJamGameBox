using Core.Inventory.Item;
using Core.Payer;
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
        
        private ItemObjectBase _selectedItem;
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

            if (Input.GetKey(KeyCode.E))
            {
                _selectedItem.ToTake();
                IsDisplayed = false;
            }
        }

        private void ChangeStateHint(ItemObjectBase item)
        {
            IsDisplayed = item != null;
            
            if (IsDisplayed)
            {
                _selectedItem = item;
            }
        }
    }
}