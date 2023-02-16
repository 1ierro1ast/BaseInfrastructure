using UnityEngine;

namespace Codebase.Extensions
{
    public static class Vector3Extensions
    {
        public static float SqrDistance(this Vector3 from, Vector3 to)
        {
            return (to - from).sqrMagnitude;
        }

        public static bool SqrCompareDistance(this Vector3 from, Vector3 to, float borderAmount)
        {
            return SqrDistance(from, to) < borderAmount;
        }

        public static void ConvertToVector2ZAsY(this Vector3 vector3, out Vector2 to)
        {
            to = new Vector2(vector3.x, vector3.z);
        }

        public static Vector2 ConvertToVector2ZAsY(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }
    }
}