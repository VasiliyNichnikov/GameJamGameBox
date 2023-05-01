using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.SoundLogic
{
    public class SoundStorage : MonoBehaviour
    {
        [SerializeField] private List<SoundItem> _soundItems;

        public SoundItem GetSoundItemByType(SoundType type)
        {
            return _soundItems.FirstOrDefault(sound => sound.SoundType == type);
        }
    }
}