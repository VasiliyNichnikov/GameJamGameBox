using Core.Pool;
using UnityEngine;

namespace Core.UI
{
    public class DialogManager : MonoBehaviour, IDialogManager
    {
        [SerializeField]
        private Transform _parent;
        
        private ItemDialogPool _pool;

        public void Init()
        {
            _pool = new ItemDialogPool();
        }
        
        public T ShowDialog<T>() where T : DialogBase
        {
            var dialog = _pool.GetOrCreateDialog<T>(_parent);
            dialog.InitHidePool(() => _pool.HideDialog(dialog));
            dialog.Show();
            return dialog;
        }

        public T ShowStoryDialog<T>() where T : DialogBase
        {
            return null;
        }
    }
}