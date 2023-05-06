using System;
using Core.Pool;
using UnityEngine;

namespace Core.UI
{
    public abstract class DialogBase : MonoBehaviour, IDisposable, IPoolObject
    {
        private bool _isInitialized;
        private Action _onHideDialog;

        public bool IsClosed { get; private set; }
        
        public void InitHidePool(Action onHideDialog)
        {
            _onHideDialog = onHideDialog;
        }
        
        protected virtual void Init()
        {
            if (_isInitialized)
            {
                return;
            }

            Main.Instance.InputHandler.OnInputKeyboard += OnInputSpaceButton;
            _isInitialized = true;
        }

        public void Hide()
        {
            _onHideDialog?.Invoke();
            gameObject.SetActive(false);
        }

        public void Die()
        {
            Dispose();
        }

        private void OnInputSpaceButton(KeyCode code)
        {
            if (code == KeyCode.Space)
            {
                Hide();
                IsClosed = true;
            }
        }

        public virtual void Dispose()
        {
            Main.Instance.InputHandler.OnInputKeyboard -= OnInputSpaceButton;
        }

        private void OnDestroy()
        {
            Die();
        }

        public void Show()
        {
            IsClosed = false;
            gameObject.SetActive(true);
        }
    }
}