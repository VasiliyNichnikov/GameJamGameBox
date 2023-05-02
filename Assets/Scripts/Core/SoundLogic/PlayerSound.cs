using System;
using Core.Pool;
using UnityEngine;

namespace Core.SoundLogic
{
    public class PlayerSound : MonoBehaviour, IPlayerSound
    {
        [SerializeField] private Transform _soundParent;

        private SoundPool _pool;

        private void Start()
        {
            _pool = new SoundPool(_soundParent);
        }

        public void PlayEffect(AudioClip clip)
        {
            var sound = _pool.GetOrCreateObject(SoundType.EffectSound);
            sound.Show();
            sound.Init(() => _pool.HideObject(sound));
            sound.Play(clip);
        }
    }
}