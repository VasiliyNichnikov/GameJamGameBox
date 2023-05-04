using Core.Inventory;
using Core.Payer;
using Core.SoundLogic;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        private void Awake()
        {
            Instance = this;
            _itemsInPlayerHandManager.Init(_inventoryManager);
            
            // Выключаем курсор
            Cursor.visible = false;
        }
    }
}