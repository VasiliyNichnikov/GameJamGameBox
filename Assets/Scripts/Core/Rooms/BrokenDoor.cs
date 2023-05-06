using UnityEngine;

namespace Core.Rooms
{
    public class BrokenDoor : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private bool _isPlayed;
        
        public void DestroyDoor()
        {
            if (_isPlayed)
            {
                Debug.LogWarning("Destroy door is already played");
                return;
            }

            _isPlayed = true;
            _audioSource.Play();
            gameObject.SetActive(false);
        }
    }
}