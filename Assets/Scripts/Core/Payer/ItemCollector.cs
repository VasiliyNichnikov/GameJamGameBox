using System;
using Core.Inventory.Item;
using UnityEngine;

namespace Core.Payer
{
    public class ItemCollector : MonoBehaviour
    {
        /// <summary>
        /// Срабатывает, когда можно подобрать предмет
        /// Если в качестве параметра будет значение null, то предмета для подбора нет
        /// </summary>
        public static event Action<ItemObjectBase> OnChangeStateActive;

        private const string ItemTag = "Item";

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(ItemTag))
            {
                return;
            }

            TryToDisplayHintCollectorButton(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(ItemTag))
            {
                return;
            }

            TryToDisplayHintCollectorButton(other.gameObject, true);
        }

        private void TryToDisplayHintCollectorButton(GameObject go, bool isExit = false)
        {
            if (!TryGetItem(go, out var item))
            {
                return;
            }
            
            var resultItem = isExit ? null : item;
            OnChangeStateActive?.Invoke(resultItem);
        }

        private bool TryGetItem(GameObject go, out ItemObjectBase item)
        {
            return go.TryGetComponent(out item);
        }
    }
}