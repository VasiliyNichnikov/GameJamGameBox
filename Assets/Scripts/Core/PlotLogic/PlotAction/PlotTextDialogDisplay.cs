using Core.UI.Quests;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.PlotLogic.PlotAction
{
    public class PlotTextDialogDisplay : PlotActionBase
    {
        public override void RunAction(JsonMessage<StepData> message)
        {
            var ext = message.GetExt<TextDialogExtensionData>();
            if (ext == null)
            {
                Debug.LogWarning($"Extension is null: (TextDialogExtensionData)");
                return;
            }

            var dialog = Main.Instance.DialogManager.ShowDialog<MessagePersonView>();
            dialog.Init(ext.Value.NamePerson, ext.Value.MessagePerson);
        }

        public override bool IsEventCompleted()
        {
            // Проверяем чтобы игрок куда либо нажад
            return true;
        }
    }
}