using Loaders;

namespace DataHelpers
{
    public class MainData : ILoader
    {
        public MapDataHelper MapHelper { get; }
        public QuestDataHelper QuestHelper { get; }
        public TriggerDataHelper TriggerHelper { get; }
        public PlotDataHelper PlotHelper { get; }
        public DoorsDataHelper DoorHelper { get; }
        public SoundSettingsDataHelper SoundDataHelper { get; }

        public MainData()
        {
            MapHelper = new MapDataHelper();
            QuestHelper = new QuestDataHelper();
            TriggerHelper = new TriggerDataHelper();
            PlotHelper = new PlotDataHelper();
            DoorHelper = new DoorsDataHelper();
            SoundDataHelper = new SoundSettingsDataHelper();
        }

        public void LoadAwake()
        {
            MapHelper.Load();
            QuestHelper.Load();
            TriggerHelper.Load();
            PlotHelper.Load();
            DoorHelper.Load();
            SoundDataHelper.Load();
        }

        public void LoadStart()
        {
        }
    }
}