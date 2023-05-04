using Core.Pool;
using UnityEngine;

namespace Core.SoundLogic
{
    public class PlayerSound : MonoBehaviour, IPlayerSound
    {
        [SerializeField] private Transform _soundParent;
        [SerializeField] private AudioClip[] _footSteps;

        private SoundItem _footStepSound;
        private SoundPool _pool;

        private void Start()
        {
            _pool = new SoundPool(_soundParent);

            InitFootSteps();
        }

        private void InitFootSteps()
        {
            _footStepSound = _pool.GetOrCreateObject(SoundType.Steps);
            _footStepSound.Show();
            _footStepSound.Init(null, 0.23f);
        }
        
        public void PlayEffect(AudioClip clip)
        {
            var sound = _pool.GetOrCreateObject(SoundType.EffectSound);
            sound.Show();
            sound.Init(() => _pool.HideObject(sound));
            sound.Play(clip);
        }

        public void PlayStep()
        {
            if (_footStepSound.IsPlaying)
            {
                return;
            }

            var clip = _footSteps[Random.Range(0, _footSteps.Length)];
            _footStepSound.Play(clip);
        }
    }
}