using UnityEngine;

namespace Codebase.Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path, Transform parent);
        
        T Instantiate<T>(string path) where T : Object;
        T Instantiate<T>(string path, Vector3 at) where T : Object;
        T Instantiate<T>(string path, Transform parent) where T : Object;
        
        GameObject Instantiate(GameObject prefab);
        GameObject Instantiate(GameObject prefab, Vector3 at);
        GameObject Instantiate(GameObject prefab, Transform parent);
        
        T Instantiate<T>(T prefab) where T : Object;
        T Instantiate<T>(T prefab, Vector3 at) where T : Object;
        T Instantiate<T>(T prefab, Transform parent) where T : Object;

        T GetObject<T>(string path) where T : Object;
        T[] GetAllObjects<T>(string path) where T : Object;
        T GetScriptableObject<T>(string path) where T : ScriptableObject;
        T[] GetAllScriptableObjects<T>(string path) where T : ScriptableObject;
        int GetAssetAmount(string path);
    }
}