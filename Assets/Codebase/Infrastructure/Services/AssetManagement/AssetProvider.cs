using UnityEngine;

namespace Codebase.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        public GameObject Instantiate(string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }

        public T Instantiate<T>(string path) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return Object.Instantiate(prefab);
        }

        public T Instantiate<T>(string path, Vector3 at) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public T Instantiate<T>(string path, Transform parent) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return Object.Instantiate(prefab, parent);
        }

        public GameObject Instantiate(GameObject prefab)
        {
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(GameObject prefab, Vector3 at)
        {
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        public GameObject Instantiate(GameObject prefab, Transform parent)
        {
            return Object.Instantiate(prefab, parent);
        }

        public T Instantiate<T>(T prefab) where T : Object
        {
            return Object.Instantiate(prefab);
        }

        public T Instantiate<T>(T prefab, Vector3 at) where T : Object
        {
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public T Instantiate<T>(T prefab, Transform parent) where T : Object
        {
            return Object.Instantiate(prefab, parent);
        }

        public T GetObject<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public T[] GetAllObjects<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }

        public T GetScriptableObject<T>(string path) where T : ScriptableObject
        {
            return Resources.Load<T>(path);
        }

        public T[] GetAllScriptableObjects<T>(string path) where T : ScriptableObject
        {
            return Resources.LoadAll<T>(path);
        }

        public int GetAssetAmount(string path)
        {
            var amount = Resources.LoadAll<GameObject>(path).Length;
            return amount;
        }
    }
}