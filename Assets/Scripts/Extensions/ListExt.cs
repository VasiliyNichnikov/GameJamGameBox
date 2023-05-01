using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class ListExt
    {
        public static Vector3 ConvertToVector3(this List<float> list)
        {
            if (list.Count != 3)
            {
                Debug.LogError("Number elements in list must be equal 3");
                return Vector3.zero;
            }

            return new Vector3(list[0], list[1], list[2]);
        }

        public static Quaternion ConvertToRotation(this List<float> list)
        {
            var vector = list.ConvertToVector3();
            return Quaternion.Euler(vector);
        }
    }
}