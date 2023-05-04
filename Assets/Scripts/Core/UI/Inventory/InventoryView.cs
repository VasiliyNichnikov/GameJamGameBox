using System;
using System.Collections.ObjectModel;
using Core.Pool;
using DG.Tweening;
using Extensions;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.UI.Inventory
{
    public class InventoryView : DialogBase
    {
        public bool IsOpened { get; private set; }

        [SerializeField] private int _numberItems;
        [SerializeField] private Transform _content;

        [Space] [SerializeField] private CanvasGroup _group;
        [SerializeField] private float _durationAnimation;

        private ItemViewPool _pool;
        private bool _isPlayedAnimation;

        private void Awake()
        {
            _pool = new ItemViewPool(_content);
            InitItems();
        }

        private void InitItems()
        {
            for (var i = 0; i < _numberItems; i++)
            {
                _pool.GetOrCreateObject(ItemViewType.Default);
            }
        }

        public void Refresh(ReadOnlyCollection<ItemData> data)
        {
            if (data.Count > _numberItems)
            {
                Debug.LogError("Number items more then number items");
                return;
            }


            for (var i = 0; i < _numberItems; i++)
            {
                var itemView = _pool.CreatedItems[i];
                if (itemView == null)
                {
                    continue;
                }

                if (i >= data.Count)
                {
                    itemView.InitEmpty();
                    continue;
                }

                var item = data[i];
                itemView.Init(item);
            }
        }

        public void OpenInventory()
        {
            if (_isPlayedAnimation)
            {
                return;
            }

            
            AnimationShowAndHide(1.0f, null);
            IsOpened = true;
            Show();
        }

        public void CloseInventory()
        {
            if (_isPlayedAnimation)
            {
                return;
            }
            IsOpened = false;
            AnimationShowAndHide(0.0f, Hide);
        }

        public override void Dispose()
        {
            // nothing
        }

        private void AnimationShowAndHide(float end, Action onEndAnimation)
        {
            _isPlayedAnimation = true;
            _group.DOFade(end, _durationAnimation).OnComplete(() =>
            {
                onEndAnimation?.Invoke();
                _isPlayedAnimation = false;
            });
        }
    }
}