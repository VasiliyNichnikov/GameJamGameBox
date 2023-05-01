using System.Linq;
using Core.Inventory.Item;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Pool
{
    public class ItemObjectPool : PoolBase<ItemObjectType, ItemObjectBase>
    {
        [CanBeNull]
        public override ItemObjectBase GetOrCreateObject(ItemObjectType soundType)
        {
            var selectedItem = UnusedObjects.FirstOrDefault(item => item.ObjectType == soundType);
            if (selectedItem != null)
            {
                UnusedObjects.Remove(selectedItem);
                UsedObjects.Add(selectedItem);
                selectedItem.Show();
                return selectedItem;
            }

            var createdItem = Main.Instance.ItemFactory.CreateItemObject(soundType);
            if (createdItem == null)
            {
                Debug.LogError($"ItemObjectPool. Not created item: {soundType}");
                return null;
            }
            UsedObjects.Add(createdItem);
            return createdItem;
        }
    }
}