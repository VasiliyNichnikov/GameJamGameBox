using Core.InputSystem;
using Core.Inventory;
using Core.Map;
using Core.PlotLogic;
using Core.Quests;
using Core.SoundLogic;
using Core.TriggerLogic;
using Core.UI;
using DataHelpers;
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
        public SoundStorage SoundStorage { get; private set; }
        public MainData Data { get; private set; }
        public IMapManager MapManager => _mapManager;
        public IQuestManager QuestManager => _questManager;
        public IDialogManager DialogManager => _dialogManager;
        public QuestsStorage QuestsStorage => _questsStorage;
        public IInputHandler InputHandler => _inputHandler;

        [SerializeField] private ItemStorage _itemStoragePrefab;
        [SerializeField] private SpriteStorage _spriteStoragePrefab;
        [SerializeField] private SoundStorage _soundStoragePrefab;
        [SerializeField] private QuestsStorage _questsStorage;
        [SerializeField] private DialogManager _dialogManager;
        [SerializeField] private InputHandler _inputHandler;
        
        [Space(10)] [SerializeField] private ItemFactory _itemFactory;
        private ItemStorage _itemStorage;

        private LoaderGameSceneAndUI _loaderGame;
        private MapManager _mapManager;
        private TriggerManager _triggerManager;
        private QuestManager _questManager;
        private PlotManager _plotManager;
        
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
            
            if (SoundStorage == null)
            {
                SoundStorage = Instantiate(_soundStoragePrefab, transform, false);
            }

            _dialogManager.Init();
            _itemFactory.Init(_itemStorage);

            // Загрзука данных в ОЗУ
            _loaderGame = new LoaderGameSceneAndUI();
            // Загружаем Json
            Data = new MainData();
            _mapManager = new MapManager(Data.MapHelper);
            // _triggerManager = new TriggerManager();
            _questManager = new QuestManager();
            // Перемещаемся в ОЗУ
            _loaderGame
                .AddLoader(Data)
                .AddLoader(_questManager)
                .AddLoader(_mapManager)
                // .AddLoader(_triggerManager)
                .StartLoading();
            
            _plotManager = new PlotManager(_questManager);
            
            // Создание кнопки E
            _dialogManager.ShowDialog<HintCollectorView>();
        }

        private void Update()
        {
            // _triggerManager.Update();
        }

        private void OnDestroy()
        {
            _plotManager.Dispose();
        }
    }
}