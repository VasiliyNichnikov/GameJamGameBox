using Core.Doors;
using Core.SoundLogic.AtmosphericSounds;
using DG.Tweening;
using Loaders.Data.Raw;
using Loaders.Data.Ready;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.SoundLogic
{
    public class BackgroundSoundManager : IBackgroundSoundManager
    {
        #region MixerValues

        private const string BackgroundHallValue = "BackgroundHallValue";
        private const string BackgroundEnemyAmbienceValue = "BackgroundEnemyAmbienceValue";

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
            // todo говнокод
            // Будем менять дорожку в миксере
            // Включать звук сердца на фоне
            // Проигрывать громкие звуки
            // todo перенести все в настройки звука
            _mixer.audioMixer.DOSetFloat(BackgroundEnemyAmbienceValue, 0, 5.5f);
        }

        public void EscapeFromEnemy()
        {
            // Уход от врага сопровождается тишиной
            // Пока игрок не зайдет к комнату
            // todo перенести все в настройки звука
            _mixer.audioMixer.DOSetFloat(BackgroundEnemyAmbienceValue, -80, 2.5f);
        }
        
        public void EntranceToRoom(ISoundRoom roomSound)
        {
            // todo подумать над выключением звуков при встрече с монстром
            if (roomSound == null || !roomSound.IsIgnoreOffAmbiences)
            {
                ChangeVolumeAudio(BackgroundHallValue, _data.InRoom);
            }
            
            roomSound?.StartBackground();
        }

        public void ExitFromRoomToHall(ISoundRoom roomSound)
        {
            // todo подумать над выключением звуков при встрече с монстром
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