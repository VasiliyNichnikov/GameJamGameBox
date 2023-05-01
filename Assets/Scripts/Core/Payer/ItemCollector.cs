using System;
using Core.Inventory.Item;
using Core.Quests;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Payer
{
    public class ItemCollector : MonoBehaviour
    {
        public readonly struct ObjectInTrigger
        {
            [CanBeNull] public readonly ItemObjectBase Item;
            [CanBeNull] public readonly InteractionObjectBase InteractionObject;

            public ObjectInTrigger(ItemObjectBase item)
            {
                Item = item;
                InteractionObject = null;
            }

            public ObjectInTrigger(InteractionObjectBase interactionObject)
            {
                Item = null;
                InteractionObject = interactionObject;
            }

            private ObjectInTrigger(ItemObjectBase item, InteractionObjectBase interactionObject)
            {
                Item = item;
                InteractionObject = interactionObject;
            }

            public bool IsItemEmpty()
            {
                return Item == null;
            }

            public bool IsInteractionEmpty()
            {
                return InteractionObject == null;
            }
            
            public bool IsEmpty()
            {
                return IsItemEmpty() && IsInteractionEmpty();
            }

            public static ObjectInTrigger Empty()
            {
                return new ObjectInTrigger(null, null);
            }
        }
        
        /// <summary>
        /// Срабатывает, когда можно подобрать предмет
        /// Если в качестве параметра будет значение null, то предмета для подбора нет
        /// </summary>
        public static event Action<ObjectInTrigger> OnChangeStateActive;

        private const string ItemTag = "Item";
        private const string InteractionTag = "Interaction";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ItemTag))
            {
                TryToDisplayHintCollectorButton<ItemObjectBase>(other.gameObject);
                return;
            }

            if (other.CompareTag(InteractionTag))
            {
                TryToDisplayHintCollectorButton<InteractionObjectBase>(other.gameObject);
                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ItemTag))
            {
                TryToDisplayHintCollectorButton<ItemObjectBase>(other.gameObject, true);
                return;
            }
            
            if (other.CompareTag(InteractionTag))
            {
                TryToDisplayHintCollectorButton<InteractionObjectBase>(other.gameObject, true);
                return;
            }
        }

        private void TryToDisplayHintCollectorButton<T>(GameObject go, bool isExit = false) where T: Object
        {
            if (!go.TryGetComponent(out T component))
            {
                return;
            }

            if (isExit)
            {
                var data = ObjectInTrigger.Empty();
                OnChangeStateActive?.Invoke(data);
            }
            else
            {
                var data = new ObjectInTrigger();
                switch (component)
                {
                    case ItemObjectBase item:
                    {
                        data = new ObjectInTrigger(item);
                        break;
                    }
                    case InteractionObjectBase interactionObject:
                    {
                        data = new ObjectInTrigger(interactionObject);
                        break;
                    }
                    default:
                        Debug.LogError("ItemCollector. Not found type object");
                        break;
                }

                OnChangeStateActive?.Invoke(data);
            }
        }
    }
}