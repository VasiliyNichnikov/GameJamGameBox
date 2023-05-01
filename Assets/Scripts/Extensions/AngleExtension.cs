using UnityEngine;

namespace Extensions
{
    public static class AngleExtension
    {
        public static float ClampAngle(this float angle, float min, float max)
        {
            if (angle < -360f)
            {
                angle += 360f;
            }

            if (angle > 360)
            {
                angle -= 360;
            }

            return Mathf.Clamp(angle, min, max);
        }

        public static Vector3 ClampAngle(this Vector3 angle, float min, float max)
        {
            return new Vector3(angle.x.ClampAngle(min, max), angle.y.ClampAngle(min, max), angle.z.ClampAngle(min, max));
        }
    }
}