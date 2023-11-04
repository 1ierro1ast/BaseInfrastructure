﻿using UnityEngine;

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
        
        public static Vector2 GetRandomPointAtCircle(this Vector2 vector2, float radius, Vector2 center = default)
        {
            if (center == default) center = vector2;
            vector2.x = Random.value - 0.5f;
            vector2.y = Random.value - 0.5f;
            vector2 = vector2.normalized * radius;
            return vector2 + center;
        }

        public static Vector2 GetRandomPointAtCircle(this Vector2 vector2, float radius, Vector3 center = default)
        {
            return GetRandomPointAtCircle(vector2, radius, new Vector2(center.x, center.z));
        }
        
        public static Vector2 GetMiddlePoint(this Vector2 startPoint, Vector2 finishPoint)
        {
            return (finishPoint - startPoint) / 2f;
        }
    }
}