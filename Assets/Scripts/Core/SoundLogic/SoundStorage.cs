using System;
using System.Collections.Generic;
using System.Linq;
using Core.Doors;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.SoundLogic
{
    public class SoundStorage : MonoBehaviour
    {
        [Serializable]
        public struct SoundNoiseForRoom
        {
            public RoomType RoomType => _room;
            
            [SerializeField] private AudioClip[] _clips;
            [SerializeField] private RoomType _room;

            private readonly List<int> _usedSounds;

            [CanBeNull]
            public AudioClip GetUnusedNoise()
            {
                if (_usedSounds.Count == _clips.Length || _clips.Length == 0)
                {
                    return null;
                }

                var plug = 1000;
                var clipId = 0;
                while (true)
                {
                    clipId = Random.Range(0, _clips.Length);
                    if (!_usedSounds.Contains(clipId))
                    {
                        break;
                    }

                    plug--;
                    if (plug == 0)
                    {
                        Debug.LogError("Number operations more permissible!");
                        return null;
                    }
                }

                var selectedClip = _clips[clipId];
                return selectedClip;
            }
        }

        [SerializeField] private List<SoundNoiseForRoom> _noises;
        [SerializeField] private List<SoundItem> _soundItems;

        public SoundItem GetSoundItemByType(SoundType type)
        {
            return _soundItems.FirstOrDefault(sound => sound.SoundType == type);
        }

        [CanBeNull]
        public AudioClip GetAudioClipByRoom(RoomType roomType)
        {
            var roomWithNoise = _noises.FirstOrDefault(noise => noise.RoomType == roomType);
            return roomWithNoise.GetUnusedNoise();
        }
    }
}