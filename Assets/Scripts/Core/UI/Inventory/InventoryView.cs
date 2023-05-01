using System.Collections.ObjectModel;
using Core.Pool;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.UI.Inventory
{
    public class InventoryView : DialogBase
    {
        [SerializeField] private Transform _content;
        
        private ItemViewPool _pool;

        private void Awake()
        {
            _pool = new ItemViewPool(_content);
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

        public override void Dispose()
        {
            // nothing
        }
    }
}