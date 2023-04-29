using System.Collections.Generic;
using System.Linq;
using Core.Inventory.Item;
using Core.Inventory.View;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Inventory
{
    public class ItemStorage : MonoBehaviour
    {
        [SerializeField] private List<ItemObjectBase> _itemObjects;
        [SerializeField] private List<ItemView> _itemViews;
        
        [CanBeNull]
        public ItemObjectBase GetPrefabByType(ItemObjectType objectType)
        {
            return _itemObjects.FirstOrDefault(item => item.ObjectType == objectType);
        }

        [CanBeNull]
        public ItemView GetPrefabByType(ItemViewType type)
        {
            return _itemViews.FirstOrDefault(item => item.Type == type);
        }
    }
}