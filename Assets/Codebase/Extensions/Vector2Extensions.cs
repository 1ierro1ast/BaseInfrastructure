using UnityEngine;

namespace Codebase.Extensions
{
    public static class Vector2Extensions
    {
        public static float SqrDistance(this Vector2 from, Vector2 to)
        {
            return (to - from).sqrMagnitude;
        }

        public static bool SqrCompareDistance(this Vector2 from, Vector2 to, float borderAmount)
        {
            return SqrDistance(from, to) < borderAmount;
        }

        public static void ConvertToVector3YAsZ(this Vector2 vector2, out Vector3 to)
        {
            to = new Vector3(vector2.x, 0, vector2.y);
        }

        public static Vector3 ConvertToVector3YAsZ(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0, vector2.y);
        }

        public static void ConvertToVector3YAsZ(this Vector2 vector2, out Vector3 to, float y)
        {
            to = new Vector3(vector2.x, y, vector2.y);
        }

        public static Vector3 ConvertToVector3YAsZ(this Vector2 vector2, float y)
        {
            return new Vector3(vector2.x, y, vector2.y);
        }
    }
}