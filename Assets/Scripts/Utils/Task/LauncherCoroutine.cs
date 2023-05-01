using UnityEngine;

namespace Utils.Task
{
    public class LauncherCoroutine : MonoBehaviour
    {
        public static MonoBehaviour CoroutineHost { get; private set; }

        private void Awake()
        {
            CoroutineHost = this;
        }
    }
}