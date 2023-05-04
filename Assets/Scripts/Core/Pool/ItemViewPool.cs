using System.Collections.Generic;
using System.Linq;
using Core.UI;
using Core.UI.Inventory;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Pool
{
    public class ItemViewPool : PoolBase<ItemViewType, ItemView>
    {
        public IList<ItemView> CreatedItems => UsedObjects;

        private readonly Transform _itemParent;
        
        public ItemViewPool(Transform itemParent)
        {
            _itemParent = itemParent;
        }

        [CanBeNull]
        public override ItemView GetOrCreateObject(ItemViewType soundType)
        {
            var selectedItem = UnusedObjects.FirstOrDefault(item => item.Type == soundType);
            if (selectedItem != null)
            {
                UnusedObjects.Remove(selectedItem);
                UsedObjects.Add(selectedItem);
                selectedItem.Show();
                return selectedItem;
            }


            var createdItem = Main.Instance.ItemFactory.CreateItemView(soundType, _itemParent);
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