using System.Linq;
using Core.Inventory.View;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Pool
{
    public class ItemViewPool : PoolBase<ItemViewType, ItemView>
    {
        [CanBeNull]
        public override ItemView GetOrCreateObject(ItemViewType typeObject)
        {
            var selectedItem = UnusedObjects.FirstOrDefault(item => item.Type == typeObject);
            if (selectedItem != null)
            {
                UnusedObjects.Remove(selectedItem);
                UsedObjects.Add(selectedItem);
                selectedItem.Show();
                return selectedItem;
            }


            var createdItem = Main.Instance.ItemFactory.CreateItemView(typeObject);
            if (createdItem == null)
            {
                Debug.LogError($"ItemObjectPool. Not created item: {typeObject}");
                return null;
            }

            UsedObjects.Add(createdItem);
            return createdItem;
        }
    }
}