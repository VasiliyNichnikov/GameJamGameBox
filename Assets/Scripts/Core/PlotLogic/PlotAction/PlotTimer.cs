using System.Collections;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.PlotLogic.PlotAction
{
    public class PlotTimer : PlotActionBase
    {
        private float _leftTime;
        private IEnumerator _activeTimer;

        public override void RunAction(JsonMessage<StepData> message)
        {
            var ext = message.GetExt<TimerExtensionData>();
            if (ext == null)
            {
                Debug.LogWarning($"Extension is null: (PlotTimer)");
                return;
            }

            _leftTime = ext.Value.Time;
            if (_activeTimer == null)
            {
                _activeTimer = CountDown();
                Main.Instance.StartCoroutine(_activeTimer);
            }
            else
            {
                Debug.LogWarning("Count down is not null");
            }
        }

        public override bool IsEventCompleted()
        {
            return _activeTimer == null;
        }

        private IEnumerator CountDown()
        {
            yield return new WaitForSeconds(_leftTime);
            _activeTimer = null;
        } 
    }
}