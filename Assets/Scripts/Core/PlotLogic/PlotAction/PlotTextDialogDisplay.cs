using Core.UI;
using Core.UI.Quests;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.PlotLogic.PlotAction
{
    public class PlotTextDialogDisplay : PlotActionBase
    {
        private MessagePersonView _dialog;
        
        public override void RunAction(JsonMessage<StepData> message)
        {
            var ext = message.GetExt<TextDialogExtensionData>();
            if (ext == null)
            {
                Debug.LogWarning($"Extension is null: (TextDialogExtensionData)");
                return;
            }

            _dialog = Main.Instance.DialogManager.ShowDialog<MessagePersonView>();
            _dialog.Init(ext.Value.NamePerson, ext.Value.MessagePerson, ext.Value.ShowExitButton);
        }

        public override bool IsEventCompleted()
        {
            if (_dialog == null)
            {
                Debug.LogWarning("Dialog is null");
                return true;
            }
            
            // Проверяем чтобы игрок куда либо нажад
            return _dialog.IsClosed;
        }
    }
}