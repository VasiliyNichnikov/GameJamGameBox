using System;
using Core.Pool;
using UnityEngine;

namespace Core.UI
{
    public abstract class DialogBase : MonoBehaviour, IDisposable, IPoolObject
    {
        public abstract void Dispose();
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Die()
        {
            // nothing
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}