using Core.Pool;
using UnityEngine;

namespace Core.UI
{
    public class DialogManager : MonoBehaviour, IDialogManager
    {
        [SerializeField]
        private Transform _parent;

        [SerializeField] private RectTransform _holder;
        
        private ItemDialogPool _pool;

        public void Init()
        {
            _pool = new ItemDialogPool();
        }
        
        public T ShowDialog<T>() where T : DialogBase
        {
            var holderForDialog = Instantiate(_holder, _parent, false);
            holderForDialog.name = $"{holderForDialog.name}_holder";
            
            var dialog = _pool.GetOrCreateDialog<T>(holderForDialog);
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