using UnityEngine;

namespace Utils
{
    public static class Vector3Utils
    {
        public static Vector3 GetVectorFromAngle(float angle)
        {
            var angleRad = angle * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
        }
    }
}