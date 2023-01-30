namespace Codebase.Infrastructure.Services.SaveLoad
{
    public interface IGetSetPrefsService : IService
    {
        void SetInt(string saveKey, int value);
        bool TryLoadInt(string saveKey, out int value);
        int GetInt(string saveKey, int defaultValue);
        
        void SetFloat(string saveKey, float value);
        bool TryGetFloat(string saveKey, out float value);
        float GetFloat(string saveKey, float defaultValue);
        
        void SetString(string saveKey, string value);
        bool TryGetString(string saveKey, out string value);
        string GetString(string saveKey, string defaultValue);
        
        void SetBool(string saveKey, bool value);
        bool TryGetBool(string saveKey, out bool value);
        bool GetBool(string saveKey, bool defaultValue);
        
        void CleanAllData();
    }
}