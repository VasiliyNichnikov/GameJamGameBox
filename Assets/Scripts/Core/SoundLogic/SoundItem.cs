using System;
using System.Collections;
using Core.Pool;
using UnityEngine;

namespace Core.SoundLogic
{
    public class SoundItem : MonoBehaviour, IPoolObject
    {
        public bool IsPlaying { get; private set; }
        
        public SoundType SoundType => _soundType;

        [SerializeField] private SoundType _soundType;
        [SerializeField] private AudioSource _source;

        private IEnumerator _checkPlayerMusic;
        private Action _onMusicPlayed;
        private float _additionalDelay;
        
        public void Init(Action onMusicPlayed, float additionalDelay = 0)
        {
            _onMusicPlayed = onMusicPlayed;
            _additionalDelay = additionalDelay;
        }
        
        public void Play(AudioClip clip)
        {
            if (_checkPlayerMusic != null)
            {
                Debug.LogWarning("PlayerMusic is not null");
                return;
            }

            _checkPlayerMusic = CheckPlayingMusic();
            _source.clip = clip;
            StartCoroutine(_checkPlayerMusic);
            _source.PlayOneShot(clip);
        }
        
        public void Hide()
        {
            _source.Stop();
            _checkPlayerMusic = null;
            gameObject.SetActive(false);
        }

        public void Die()
        {
        }

        public void Show()
        {
            _source.Stop();
            _checkPlayerMusic = null;
            gameObject.SetActive(true);
        }

        private IEnumerator CheckPlayingMusic()
        {
            IsPlaying = true;
            var seconds = _source.clip.length + _additionalDelay;
            yield return new WaitForSeconds(seconds);
            _onMusicPlayed?.Invoke();
            IsPlaying = false;
            _checkPlayerMusic = null;
        }
    }
}