using System;
using System.Collections;
using Core.Pool;
using UnityEngine;

namespace Core.SoundLogic
{
    public class SoundItem : MonoBehaviour, IPoolObject
    {
        public SoundType SoundType => _soundType;

        [SerializeField] private SoundType _soundType;
        [SerializeField] private AudioSource _source;

        private IEnumerator _checkPlayerMusic;
        private Action _onMusicPlayed;
        
        public void Init(Action onMusicPlayed)
        {
            _onMusicPlayed = onMusicPlayed;
        }
        
        public void Play(AudioClip clip)
        {
            if (_checkPlayerMusic == null)
            {
                Debug.LogWarning("PlayerMusic is null");
                return;
            }

            _source.clip = clip;
            _source.PlayOneShot(clip);
            StartCoroutine(_checkPlayerMusic);
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
            _checkPlayerMusic = CheckPlayingMusic();
            gameObject.SetActive(true);
        }

        private IEnumerator CheckPlayingMusic()
        {
            yield return new WaitForSeconds(_source.clip.length);
            _onMusicPlayed?.Invoke();
        }
    }
}