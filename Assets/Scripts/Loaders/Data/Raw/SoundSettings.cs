namespace Loaders.Data.Raw
{
    public struct SoundState
    {
        public float Duration { get; set; }
        public float Volume { get; set; }
    }

    public struct BackgroundSettings
    {
        public SoundState InRoom { get; set; }
        public SoundState OutRoom { get; set; }
    }
    
    public struct SoundSettings
    {
        public BackgroundSettings BackgroundSettings { get; set; }
    }
}