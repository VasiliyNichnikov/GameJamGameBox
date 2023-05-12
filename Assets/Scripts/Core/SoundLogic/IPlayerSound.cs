using Core.Payer;
using UnityEngine;

namespace Core.SoundLogic
{
    public interface IPlayerSound : INoisyObject
    {
        void PlayEffect(AudioClip clip);
        void PlayStep(PlayerSound.StepType stepType);
    }
}