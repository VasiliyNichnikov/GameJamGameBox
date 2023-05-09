namespace Core.Payer
{
    /// <summary>
    /// Объекты, издающие звук
    /// </summary>
    public interface INoisyObject
    {
        void MakeSound(float noiseVolume);
    }
}