using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.PlotLogic.PlotAction
{
    public class PlotOpenDoor : PlotActionBase
    {
        public override void RunAction(JsonMessage<StepData> message)
        {
            var ext = message.GetExt<OpenDoorExtensionData>();
            if (ext == null)
            {
                Debug.LogWarning($"Extension is null: (OpenDoorExtensionData)");
                return;
            }

            var objectForChange = Main.Instance.MapManager.GetObjectForChanges(ext.Value.NameDoor);
            if (objectForChange == null)
            {
                Debug.LogError($"Not found door: {ext.Value.NameDoor}");
                return;
            }
            objectForChange.OpenDoor();
        }

        public override bool IsEventCompleted()
        {
            return true;
        }
    }
}