using System;

namespace Core.Payer
{
    public class PlayerNoiseCatcher : INoisyObject
    {
        public static event Action<float> OnMakingSound;
        
        public void MakeSound(float noiseVolume)
        {
            OnMakingSound?.Invoke(noiseVolume);
        }
    }
}