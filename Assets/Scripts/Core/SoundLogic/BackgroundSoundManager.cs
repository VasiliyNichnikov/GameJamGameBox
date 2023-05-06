using Core.Doors;
using Core.SoundLogic.AtmosphericSounds;
using DG.Tweening;
using Loaders.Data.Ready;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.SoundLogic
{
    public class BackgroundSoundManager : IBackgroundSoundManager
    {
        #region MixerValues

        private const string BackgroundHallValue = "BackgroundHallValue";

        #endregion

        private readonly AudioMixerGroup _mixer;
        private readonly BackgroundSettingsData _data;
        
        public BackgroundSoundManager(AudioMixerGroup mixer, BackgroundSettingsData data)
        {
            _mixer = mixer;
            _data = data;
        }
        

        public void MeetingWithEnemy()
        {
            // Будем менять дорожку в миксере
            // Включать звук сердца на фоне
            // Проигрывать громкие звуки
        }

        public void EscapeFromEnemy()
        {
            // Уход от врага сопровождается тишиной
            // Пока игрок не зайдет к комнату
        }
        
        public void EntranceToRoom(ISoundRoom roomSound)
        {
            if (roomSound == null || !roomSound.IsIgnoreOffAmbiences)
            {
                ChangeVolumeAudio(BackgroundHallValue, _data.InRoom);
            }
            
            roomSound?.StartBackground();
        }

        public void ExitFromRoomToHall(ISoundRoom roomSound)
        {
            if (roomSound == null || !roomSound.IsIgnoreOffAmbiences)
            {
                ChangeVolumeAudio(BackgroundHallValue, _data.OutRoom);
            }
            
            roomSound?.EndBackground();
        }

        private void ChangeVolumeAudio(string nameAudio, BackgroundSettingsData.SoundStateData soundState)
        {
            _mixer.audioMixer.DOSetFloat(nameAudio, soundState.Volume, soundState.Duration);
        }

        private float TryGetValueFromAudioMixer(string nameValue)
        {
            var condition = _mixer.audioMixer.GetFloat(nameValue, out var result);
            if (!condition)
            {
                Debug.LogWarning($"Not found value with name: {result}");
                return 0.0f;
            }

            return result;
        }
    }
}