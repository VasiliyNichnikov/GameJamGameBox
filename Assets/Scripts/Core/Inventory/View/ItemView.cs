using Core.Inventory.Data;
using Core.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Inventory.View
{
    public class ItemView : MonoBehaviour, IPoolObject
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _title;
        [SerializeField] private Text _desciption;

        public ItemViewType Type => ItemViewType.Default;

        public void Init(ItemData itemData)
        {
            _title.text = itemData.Title;
            _desciption.text = itemData.Description;
            _icon.sprite = Main.Instance.SpriteStorage.GetSpriteByName(itemData.NameIcon);
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