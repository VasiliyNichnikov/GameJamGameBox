using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.PlotLogic.PlotAction
{
    public class PlotCreateItem : PlotActionBase
    {
        public override void RunAction(JsonMessage<StepData> message)
        {
            var ext = message.GetExt<CreateItemExtensionData>();
            if (ext == null)
            {
                Debug.LogError("Extension is null (CreateItemExtensionData)");
                return;
            }

            CreateItem(ext.Value);
        }

        private void CreateItem(CreateItemExtensionData data)
        {
            Main.Instance.MapManager.AddItemOnScene(data.ItemId, data.Position, data.Scale, data.Rotation);
        }

        public override bool IsEventCompleted()
        {
            return true;
        }
    }
}