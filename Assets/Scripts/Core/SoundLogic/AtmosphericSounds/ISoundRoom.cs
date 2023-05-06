namespace Core.SoundLogic.AtmosphericSounds
{
    public interface ISoundRoom
    {
        void StartBackground();
        void EndBackground();
        bool IsIgnoreOffAmbiences { get; }
    }
}