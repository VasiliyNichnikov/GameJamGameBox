using UnityEngine;

namespace Core.SoundLogic
{
    public interface IPlayerSound
    {
        void PlayEffect(AudioClip clip);
        void PlayStep();
    }
}