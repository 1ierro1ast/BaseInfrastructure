using UnityEngine;

namespace Codebase.Extensions
{
    public static class GameObjectExtensions
    {
        public static Component GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent(out T tcomponent))
            {
                return tcomponent;
            }

            return gameObject.AddComponent<T>();
        }
    }
}
