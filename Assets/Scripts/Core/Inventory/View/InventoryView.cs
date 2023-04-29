using System.Collections.ObjectModel;
using Core.Inventory.Data;
using Core.Pool;
using UnityEngine;

namespace Core.Inventory.View
{
    public class InventoryView : MonoBehaviour
    {
        private ItemViewPool _pool;

        private void Awake()
        {
            _pool = new ItemViewPool();
        }
        
        public void Refresh(ReadOnlyCollection<ItemData> data)
        {
            _pool.HideAll();
            foreach (var item in data)
            {
                var itemView = _pool.GetOrCreateObject(item.ViewType);
                if (itemView == null)
                {
                    continue;
                }

                itemView.Init(item);
            }
        }
    }
}