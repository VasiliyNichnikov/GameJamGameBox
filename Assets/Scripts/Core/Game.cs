using Core.Inventory;
using Core.Monster;
using Core.Payer;
using Core.SoundLogic;
using UnityEngine;

namespace Core
{
    /// <summary>
    ///  Синглтон игры, хранит менеджеры, нужные для работы игры
    /// </summary>
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        public IInventoryManager InventoryManager => _inventoryManager;
        public IPlayerSound PlayerSound => _playerSound;

        [SerializeField] private ItemsInPlayerHandManager _itemsInPlayerHandManager;
        [SerializeField] private InventoryManager _inventoryManager;
        [SerializeField] private PlayerSound _playerSound;
        [SerializeField] private MonsterBrain _monsterBrain;
        [SerializeField] private Transform _playerTransform;
        
        private void Awake()
        {
            Instance = this;
            _itemsInPlayerHandManager.Init(_inventoryManager);
            _monsterBrain.Init(_playerTransform);
            
            // Выключаем курсор
            Cursor.visible = false;
        }
    }
}