using System;
using Core.Pool;
using Loaders.Data.Ready;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Inventory
{
    public class ItemView : MonoBehaviour, IPoolObject
    {
        [SerializeField] private Image _background;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Image _icon;
        [SerializeField] private Text _title;
        [SerializeField] private Text _desciption;

        public ItemViewType Type => ItemViewType.Default;

        private void Start()
        {
            _background.gameObject.SetActive(true);
            _icon.gameObject.SetActive(true);
            _title.gameObject.SetActive(false);
            _desciption.gameObject.SetActive(false);
            
            _icon.sprite = _defaultSprite;
        }

        public void Init(ItemData itemData)
        {
            _title.text = itemData.Title;
            _desciption.text = itemData.Description;
            _icon.sprite = Main.Instance.SpriteStorage.GetSpriteByName(itemData.NameIcon);
        }

        public void InitEmpty()
        {
            _icon.sprite = _defaultSprite;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Die()
        {
            Hide();
            Destroy(gameObject);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}