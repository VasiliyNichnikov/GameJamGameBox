using Loaders;
using Loaders.Data.Raw;
using Loaders.Data.Ready;

namespace DataHelpers
{
    public class SoundSettingsDataHelper
    {
        public BackgroundSettingsData BackgroundSettings { get; private set; }
        private const string SoundSettingsFile = "SoundSettings";
        
        public void Load()
        {
            var soundSettings = StaticLoader.LoadData<SoundSettings>(SoundSettingsFile);
            BackgroundSettings = new BackgroundSettingsData(soundSettings.BackgroundSettings.InRoom,
                soundSettings.BackgroundSettings.OutRoom);
        }
    }
}