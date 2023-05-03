using System;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.Inventory.Item
{
    public abstract class ItemObjectBase : MonoBehaviour, IDisposable, IItemObject
    {
        protected ItemData Data;
        protected Action OnTakeAction;

        // Не можем инитить в Init, так как ObjectType вызывается на этапе раньше
        public abstract ItemObjectType ObjectType { get; }
        public bool CanTakeItem { get; private set; }

        public void Init(ItemData data, Action onTakeAction)
        {
            Data = data;
            OnTakeAction = onTakeAction;
            CanTakeItem = true;
        }

        public void InitForQuest()
        {
            CanTakeItem = false;
        }

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

        public virtual void ToTake()
        {
            OnTakeAction?.Invoke();
            Game.Instance.InventoryManager.AddItemInInventory(Data);
        }

        public abstract void Dispose();
    }
}