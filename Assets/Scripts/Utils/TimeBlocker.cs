using System;
using Core;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Блокируем действие по времеи
    /// </summary>
    public class TimeBlocker : IDisposable
    {
        public bool IsLock { get; private set; }
        private float _timeLock;
        private float _startTime;
        
        
        public TimeBlocker SetTimeLock(float timeLock)
        {
            StopLock();
            
            _timeLock = timeLock;
            return this;
        }

        public void StartLock()
        {
            IsLock = true;
            _startTime = TimeUtils.GetCurrentTimeFromStartup();
            Main.Instance.OnUpdateTime += CheckLock;
        }

        private void CheckLock(int currentTime)
        {
            IsLock = _startTime + _timeLock >= currentTime;
            if (!IsLock)
            {
                StopLock();
            }
        }

        public TimeBlocker StopLock()
        {
            if (IsLock)
            {
                Main.Instance.OnUpdateTime -= CheckLock;
            }

            IsLock = false;
            return this;
        }

        public void Dispose()
        {
            Main.Instance.OnUpdateTime -= CheckLock;
        }
    }
}