using Loaders.Data.Raw;

namespace Loaders.Data.Ready
{
    public struct BackgroundSettingsData
    {
        public struct SoundStateData
        {
            public readonly float Duration;
            public readonly float Volume;

            public SoundStateData(SoundState soundState)
            {
                // Желательно добавить конвертор
                Duration = soundState.Duration;
                Volume = soundState.Volume;
            }
        }
        
        public SoundStateData InRoom;
        public SoundStateData OutRoom;

        public BackgroundSettingsData(SoundState inRoom, SoundState outRoom)
        {
            InRoom = new SoundStateData(inRoom);
            OutRoom = new SoundStateData(outRoom);
        }
    }
}