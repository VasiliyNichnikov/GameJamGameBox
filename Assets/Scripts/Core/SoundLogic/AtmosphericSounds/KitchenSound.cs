﻿using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.SoundLogic.AtmosphericSounds
{
    public class KitchenSound : MonoBehaviour, ISoundRoom
    {
        [SerializeField] private AudioClip[] _soundOfDrops;
        [SerializeField] private AudioSource _sourceSink;
        [SerializeField, Range(-3f, 3f)] private float _minPitch;
        [SerializeField, Range(-3f, 3f)] private float _maxPitch;
        
        public bool IsIgnoreOffAmbiences => false;
        
        private IEnumerator _currentSoundOfDrops;

        private void Start()
        {
            _currentSoundOfDrops = TryStartSound();
        }

        // todo желательно звук стопить когда игрок далеко от комнаты
        private IEnumerator TryStartSound()
        {
            while (true)
            {
                var clip = PlaySoundOfDrops();
                if (clip != null)
                {
                    yield return new WaitForSeconds(clip.length);
                }

                yield return null;
            }
        }

        public void StartBackground()
        {
            if (_currentSoundOfDrops == null)
            {
                Debug.LogError("CurrentSound of drops is null");
                return;
            }
            
            StartCoroutine(_currentSoundOfDrops);
        }

        public void EndBackground()
        {
            if (_currentSoundOfDrops == null)
            {
                Debug.LogError("CurrentSound of drops is null");
                return;
            }

            StopCoroutine(_currentSoundOfDrops);
        }

        [CanBeNull]
        private AudioClip PlaySoundOfDrops()
        {
            if (_sourceSink.isPlaying)
            {
                return null;
            }
            
            _sourceSink.pitch = Random.Range(_minPitch, _maxPitch);
            var randomClip = _soundOfDrops[Random.Range(0, _soundOfDrops.Length)];
            _sourceSink.clip = randomClip;
            _sourceSink.PlayOneShot(randomClip);
            return randomClip;
        }
    }
}