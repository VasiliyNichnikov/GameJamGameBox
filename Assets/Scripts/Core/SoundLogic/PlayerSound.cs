using System.Collections;
using Core.Pool;
using UnityEngine;

namespace Core.SoundLogic
{
    public class PlayerSound : MonoBehaviour, IPlayerSound
    {
        [SerializeField] private Transform _soundParent;
        [SerializeField] private AudioClip[] _footSteps;
        
        [Space]
        [SerializeField] private AudioSource[] _ambiencesSource;
        [SerializeField, Header("За сколько секунд до конца будем менять звуки"), Range(0, 5)] 
        private float _transitionTime;
        
        private SoundItem _footStepSound;
        private SoundPool _pool;

        private void Start()
        {
            _pool = new SoundPool(_soundParent);

            InitFootSteps();

            StartCoroutine(StartAmbiences());
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

        private IEnumerator StartAmbiences()
        {
            var currentSoundId = 0;
            while (true)
            {
                var randomId = Random.Range(0, _ambiencesSource.Length);
                if (randomId == currentSoundId)
                {
                    randomId++;
                }

                if (randomId == _ambiencesSource.Length)
                {
                    randomId = 0;
                }

                currentSoundId = randomId;
                _ambiencesSource[randomId].Play();
                yield return new WaitForSeconds(_ambiencesSource[randomId].clip.length - _transitionTime);
            }
        }
    }
}