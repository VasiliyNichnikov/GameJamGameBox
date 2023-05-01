using Core.Pool;
using UnityEngine;

namespace Core.SoundLogic
{
    public class SoundItem : MonoBehaviour, IPoolObject
    {
        public SoundType SoundType => _soundType;

        [SerializeField] private SoundType _soundType;
        [SerializeField] private AudioSource _source;

        public void Play(AudioClip clip)
        {
            _source.PlayOneShot(clip);
        }
        
        public void Hide()
        {
            _source.Stop();
            gameObject.SetActive(false);
        }

        public void Die()
        {
        }

        public void Show()
        {
            _source.Stop();
            gameObject.SetActive(true);
        }
    }
}