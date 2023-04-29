using Core.Inventory;
using Core.UI;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Самый плохой, но нужный класс в игре)
    /// Это синглтон, хранящий самые важные вещи
    /// Если считаешь, что твой код, должен быть доступен из любой точки игры, добавляй
    /// </summary>
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        public ItemFactory ItemFactory => _itemFactory;
        public SpriteStorage SpriteStorage { get; private set; }

        [SerializeField] private ItemStorage _itemStoragePrefab;
        [SerializeField] private SpriteStorage _spriteStoragePrefab;

        [Space(10)] [SerializeField] private ItemFactory _itemFactory;
        private ItemStorage _itemStorage;

        public void Awake()
        {
            Instance = this;

            if (_itemStorage == null)
            {
                _itemStorage = Instantiate(_itemStoragePrefab, transform, false);
            }

            if (SpriteStorage == null)
            {
                SpriteStorage = Instantiate(_spriteStoragePrefab, transform, false);
            }

            _itemFactory.Init(_itemStorage);
        }
    }
}