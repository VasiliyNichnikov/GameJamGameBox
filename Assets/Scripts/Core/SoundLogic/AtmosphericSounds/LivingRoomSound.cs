using UnityEngine;

namespace Core.SoundLogic.AtmosphericSounds
{
    public class LivingRoomSound : MonoBehaviour, ISoundRoom
    {
        public bool IsIgnoreOffAmbiences => true;
        [SerializeField] private AudioSource _fire;


        public void StartBackground()
        {
            _fire.enabled = true;
        }

        public void EndBackground()
        {
            _fire.enabled = false;
        }
    }
}