using System;
using Core.Doors;
using Core.InputSystem;
using Core.Inventory;
using Core.Map;
using Core.PlotLogic;
using Core.Quests;
using Core.Rooms;
using Core.SoundLogic;
using Core.TriggerLogic;
using Core.UI;
using DataHelpers;
using UnityEngine;
using UnityEngine.Audio;
using Utils;

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

        public event Action<int> OnUpdateTime; 

        public ItemFactory ItemFactory => _itemFactory;
        public SpriteStorage SpriteStorage { get; private set; }
        public SoundStorage SoundStorage { get; private set; }
        public MainData Data { get; private set; }
        public IMapManager MapManager => _mapManager;
        public IQuestManager QuestManager => _questManager;
        public IDialogManager DialogManager => _dialogManager;
        public QuestsStorage QuestsStorage => _questsStorage;
        public IInputHandler InputHandler => _inputHandler;
        public DoorStorage DoorStorage => _doorStorage;
        public IBackgroundSoundManager SoundManager => _soundManager;

        [SerializeField] private ItemStorage _itemStoragePrefab;
        [SerializeField] private SpriteStorage _spriteStoragePrefab;
        [SerializeField] private SoundStorage _soundStoragePrefab;
        [SerializeField] private QuestsStorage _questsStorage;
        [SerializeField] private DialogManager _dialogManager;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private DoorStorage _doorStorage;
        [SerializeField] private AudioMixerGroup _mixerGroup;
        [SerializeField] private HouseSystem _houseSystem;
        [SerializeField] private Transform _playerTransform;


        [Space(10)] [SerializeField] private ItemFactory _itemFactory;
        private ItemStorage _itemStorage;

        private LoaderGameSceneAndUI _loaderGame;
        private MapManager _mapManager;
        private TriggerManager _triggerManager;
        private QuestManager _questManager;
        private PlotManager _plotManager;
        private BackgroundSoundManager _soundManager;

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
            _mapManager = new MapManager(Data.MapHelper, _playerTransform);
            _triggerManager = new TriggerManager();
            _questManager = new QuestManager();
            // Перемещаемся в ОЗУ
            _loaderGame
                .AddLoader(Data)
                .AddLoader(_questManager)
                .AddLoader(_mapManager)
                .AddLoader(_triggerManager)
                .StartLoadingOnAwake();

            _plotManager = new PlotManager(_questManager);

            // Создание кнопки E
            _dialogManager.ShowDialog<HintCollectorView>();
            
            // Управляющий звуками
            _soundManager = new BackgroundSoundManager(_mixerGroup, Data.SoundDataHelper.BackgroundSettings);
            _houseSystem.Init(_soundManager);
        }

        private void OnDestroy()
        {
            _plotManager.Dispose();
            _triggerManager.Dispose();
        }

        private void Start()
        {
            _loaderGame.StartLoadingOnStart();
        }

        private void Update()
        {
            _triggerManager.Update();
            OnUpdateTime?.Invoke(TimeUtils.GetCurrentTimeFromStartup());
        }
    }
}