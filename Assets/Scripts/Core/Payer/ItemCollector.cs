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

        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _maxDistance;
        
        /// <summary>
        /// Срабатывает, когда можно подобрать предмет
        /// Если в качестве параметра будет значение null, то предмета для подбора нет
        /// </summary>
        public static event Action<ObjectInTrigger> OnChangeStateActive;

        private Camera _mainCamera;

        private const string ItemTag = "Item";
        private const string InteractionTag = "Interaction";

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            var fwd = _mainCamera.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(_mainCamera.transform.position, fwd, out var hit, _maxDistance, _layer))
            {
                var foundObject = hit.collider.gameObject;

                if (foundObject.CompareTag(ItemTag))
                {
                    TryToDisplayHintCollectorButton<ItemObjectBase>(foundObject);
                    return;
                }
                if (foundObject.CompareTag(InteractionTag))
                {
                    TryToDisplayHintCollectorButton<InteractionObjectBase>(foundObject);
                    return;
                }
            }
            // ХЗ на сколько страшно
            OnChangeStateActive?.Invoke(ObjectInTrigger.Empty());
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Camera.main == null)
            {
                return;
            }
            
            var camera = Camera.main;
            var start = camera.transform.position;
            var end = camera.transform.TransformDirection(Vector3.forward) * _maxDistance;
            Gizmos.DrawLine(start, end);
        }
#endif
        
        private void TryToDisplayHintCollectorButton<T>(GameObject go) where T : Object
        {
            if (!go.TryGetComponent(out T component))
            {
                return;
            }
            
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