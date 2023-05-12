using Core.Monster;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.PlotLogic.PlotAction
{
    public class PlotActionMonster : PlotActionBase
    {
        public override void RunAction(JsonMessage<StepData> message)
        {
            var ext = message.GetExt<ActionMonsterExtensionData>();
            if (ext == null)
            {
                Debug.LogWarning($"Extension is null: (ActionMonsterExtensionData)");
                return;
            }

            if (ext.Value.Type == ActionMonsterType.MonsterLauncher)
            {
                Game.Instance.StartMonster();
                return;
            }
        }

        public override bool IsEventCompleted()
        {
            return true;
        }
    }
}