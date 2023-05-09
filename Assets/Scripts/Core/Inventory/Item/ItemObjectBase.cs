using System;
using Core.Payer;
using Core.Pool;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.Inventory.Item
{
    public abstract class ItemObjectBase : MonoBehaviour, IDisposable, IPoolObject, INoisyObject
    {
        public string InfoAboutItem { get; private set; }
        protected ItemData Data;
        protected Action OnTakeAction;

        // Не можем инитить в Init, так как ObjectType вызывается на этапе раньше
        public abstract ItemObjectType ObjectType { get; }
        public bool CanTakeItem { get; private set; }
        public bool HasAnalogInHand { get; private set; }

        public void Init(ItemData data, Action onTakeAction)
        {
            Data = data;
            OnTakeAction = onTakeAction;
            CanTakeItem = true;
            HasAnalogInHand = data.AddToHand;
            InfoAboutItem = Data.Title;
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
            MakeSound(Data.NoisyVolume);
            Game.Instance.InventoryManager.AddItemInInventory(Data);
        }

        /// <summary>
        /// Реализовать, если предмет подбираается в руку
        /// </summary>
        public virtual void AddToHand()
        {
            // nothing
        }

        public abstract void Dispose();

        public override bool Equals(object other)
        {
            if (other is ItemObjectBase itemObject)
            {
                return Data.ObjectType == itemObject.ObjectType && Data.Id == itemObject.Data.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Data.Id.GetHashCode();
        }

        public void MakeSound(float noiseVolume)
        {
            Game.Instance.PlayerSound.MakeSound(noiseVolume);
        }
    }
}