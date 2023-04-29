using System;
using UnityEngine;

namespace Core.Inventory.Item
{
    public abstract class ItemObjectBase : MonoBehaviour, IDisposable, IItemObject
    {
        public abstract ItemObjectType ObjectType { get; }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Die()
        {
            Dispose();
            Hide();
            Destroy(gameObject);
        }

        public abstract void ToTake();
        public abstract void Dispose();
    }
}