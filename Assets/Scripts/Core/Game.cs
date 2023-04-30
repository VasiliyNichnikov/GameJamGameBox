using Core.Inventory;
using Core.Map;
using Core.Payer;
using Loaders;
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
        public IMapManager MapManager { get; private set; }

        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private InventoryManager _inventoryManager;

        private LoaderManager _loaderManager;

        private void Awake()
        {
            Instance = this;

            // Инициализация загрузчиков
            _loaderManager = new LoaderManager();
            // Добавляем данные в загрузчик
            MapManager = new MapManager(_loaderManager);
            // Загружаем данные в игру
            _loaderManager.StartLoading();

            _playerManager.Init(_inventoryManager);
        }
    }
}