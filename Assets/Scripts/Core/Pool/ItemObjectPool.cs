using System.Linq;
using Core.Inventory.Item;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Pool
{
    public class ItemObjectPool : PoolBase<ItemObjectType, ItemObjectBase>
    {
        [CanBeNull]
        public override ItemObjectBase GetOrCreateObject(ItemObjectType objectTypeObject)
        {
            var selectedItem = UnusedObjects.FirstOrDefault(item => item.ObjectType == objectTypeObject);
            if (selectedItem != null)
            {
                UnusedObjects.Remove(selectedItem);
                UsedObjects.Add(selectedItem);
                selectedItem.Show();
                return selectedItem;
            }

            var createdItem = Main.Instance.ItemFactory.CreateItemObject(objectTypeObject);
            if (createdItem == null)
            {
                Debug.LogError($"ItemObjectPool. Not created item: {objectTypeObject}");
                return null;
            }
            UsedObjects.Add(createdItem);
            return createdItem;
        }
    }
}