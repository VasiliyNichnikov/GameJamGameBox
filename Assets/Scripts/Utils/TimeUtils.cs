using UnityEngine;

namespace Utils
{
    public static class TimeUtils
    {
        public static int GetCurrentTimeFromStartup()
        {
            return Mathf.RoundToInt(Time.realtimeSinceStartup);
        }
    }
}