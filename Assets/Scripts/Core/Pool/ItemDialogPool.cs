using System.Collections.Generic;
using System.Linq;
using Core.UI;
using UnityEngine;

namespace Core.Pool
{
    public class ItemDialogPool
    {
        private readonly List<DialogBase> _usedDialogs = new List<DialogBase>();
        private readonly List<DialogBase> _unusedDialogs = new List<DialogBase>();


        public T GetOrCreateDialog<T>(Transform parent) where T: DialogBase
        {
            var selectedDialog = _unusedDialogs.FirstOrDefault(dialog => dialog.GetType() == typeof(T));
            if (selectedDialog != null)
            {
                _unusedDialogs.Remove(selectedDialog);
                _usedDialogs.Add(selectedDialog);
                return selectedDialog as T;
            }

            var createdDialog = DialogFactoryUI.GetDialog<T>();
            if (createdDialog == null)
            {
                Debug.LogError($"ItemDialogPool. Not created dialog: {typeof(T)}");
                return null;
            }
            createdDialog.transform.SetParent(parent, false);
            createdDialog.transform.SetAsFirstSibling();
            _usedDialogs.Add(createdDialog);
            return createdDialog;
        }
        
        public void HideDialog(DialogBase dialog)
        {
            if (_usedDialogs.Contains(dialog))
            {
                _usedDialogs.Remove(dialog);
                dialog.Hide();
                _unusedDialogs.Add(dialog);
            }
        }
    }
}