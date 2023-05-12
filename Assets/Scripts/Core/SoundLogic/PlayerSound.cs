using System;
using System.Collections;
using Core.Payer;
using Core.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.SoundLogic
{
    /// <summary>
    /// todo нужно вынести в отдельный компонент от игрока
    /// </summary>
    public class PlayerSound : MonoBehaviour, IPlayerSound
    {
        public enum StepType
        {
            Walk,
            Run
        }
        
        [SerializeField] private Transform _soundParent;
        [SerializeField] private AudioClip[] _footSteps;
        [SerializeField] private AudioClip[] _runSteps;

        [Space]
        [SerializeField] private AudioSource[] _ambiencesSource;
        [SerializeField, Header("За сколько секунд до конца будем менять звуки"), Range(0, 5)] 
        private float _transitionTime;

        private PlayerNoiseCatcher _playerNoiseCatcher;
        private SoundItem _footStepSound;
        private SoundPool _pool;

        private void Start()
        {
            _playerNoiseCatcher = new PlayerNoiseCatcher();
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

        public void PlayStep(StepType stepType)
        {
            if (_footStepSound.IsPlaying)
            {
                return;
            }

            AudioClip clip = null;
            switch (stepType)
            {
                case StepType.Walk:
                    clip = _footSteps[Random.Range(0, _footSteps.Length)];
                    break;
                case StepType.Run:
                    clip = _runSteps[Random.Range(0, _runSteps.Length)];
                    break;
            }

            if (clip == null)
            {
                Debug.LogError($"Not found clip with type: {stepType}");
                return;
            }
            
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

        public void MakeSound(float noiseVolume)
        {
            _playerNoiseCatcher.MakeSound(noiseVolume);
        }
    }
}