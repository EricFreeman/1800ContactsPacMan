using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 Clamp(this Vector3 vector, float min, float max, bool ignoreY = false)
        {
            vector.x = Mathf.Clamp(vector.x, min, max);
            vector.z = Mathf.Clamp(vector.z, min, max);

            if (!ignoreY)
            {
                vector.y = Mathf.Clamp(vector.y, min, max);
            }

            return vector;
        }
    }
}