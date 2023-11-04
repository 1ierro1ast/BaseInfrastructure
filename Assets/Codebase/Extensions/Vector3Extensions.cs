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
        
        public static Vector3 GetRandomPointAtSphere(this Vector3 vector3, float radius, Vector3 center = default)
        {
            if (center == default) center = vector3;
            vector3.x = Random.value - 0.5f;
            vector3.y = Random.value - 0.5f;
            vector3.z = Random.value - 0.5f;
            vector3 = vector3.normalized * radius;
            return vector3 + center;
        }

        public static Vector3 GetMiddlePoint(this Vector3 startPoint, Vector3 finishPoint)
        {
            return (finishPoint - startPoint) / 2f;
        }
    }
}