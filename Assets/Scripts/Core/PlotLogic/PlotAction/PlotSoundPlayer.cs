using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.PlotLogic.PlotAction
{
    public class PlotSoundPlayer : PlotActionBase
    {
        private bool _isPlayed;
        
        public override void RunAction(JsonMessage<StepData> message)
        {
            var ext = message.GetExt<SoundExtensionData>();
            if (ext == null)
            {
                _isPlayed = true;
                Debug.LogWarning($"Extension is null: (SoundExtensionData)");
                return;
            }
            PlaySound(ext.Value.Clip);
        }

        public override bool IsEventCompleted()
        {
            return _isPlayed;
        }

        private void PlaySound(AudioClip clip)
        {
            _isPlayed = true;
            Game.Instance.PlayerSound.PlayEffect(clip);
        }
    }
}