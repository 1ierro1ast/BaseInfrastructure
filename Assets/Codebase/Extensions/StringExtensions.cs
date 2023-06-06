using System;
using UnityEngine;

namespace Codebase.Extensions
{
    public static class StringExtensions
    {
        public static Vector3 TryConvertToVector3(this string stringVector)
        {
            string[] temp = stringVector.Substring(1, stringVector.Length - 2).Split(',');
            if (temp.Length != 3) throw new InvalidCastException();
            float x = float.Parse(temp[0]);
            float y = float.Parse(temp[1]);
            float z = float.Parse(temp[2]);
            Vector3 vector3 = new Vector3(x, y, z);
            return vector3;
        }

        public static Vector2 TryConvertToVector2(this string stringVector)
        {
            string[] temp = stringVector.Substring(1, stringVector.Length - 2).Split(',');
            if (temp.Length != 2) throw new InvalidCastException();
            float x = float.Parse(temp[0]);
            float y = float.Parse(temp[1]);
            Vector2 vector2 = new Vector2(x, y);
            return vector2;
        }
    }
}