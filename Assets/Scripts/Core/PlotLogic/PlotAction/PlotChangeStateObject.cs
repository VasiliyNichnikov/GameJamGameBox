using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.PlotLogic.PlotAction
{
    public class PlotChangeStateObject : PlotActionBase
    {
        public override void RunAction(JsonMessage<StepData> message)
        {
            var ext = message.GetExt<ChangeStateObjectExtensionData>();
            if (ext == null)
            {
                Debug.LogWarning($"Extension is null: (SoundExtensionData)");
                return;
            }

            var selectedObject = Main.Instance.MapManager.GetObjectForChanges(ext.Value.ObjectForChanges);
            if (selectedObject == null)
            {
                Debug.LogWarning("Object for changes is null");
                return;
            }

            selectedObject.ChangeState(ext.Value.State);
        }

        public override bool IsEventCompleted()
        {
            return true;
        }
    }
}