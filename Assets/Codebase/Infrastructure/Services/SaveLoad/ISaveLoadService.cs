namespace Codebase.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveInt(string saveKey, int value);
        bool TryLoadInt(string saveKey, out int value);
        int LoadInt(string saveKey, int defaultValue);
        
        void SaveFloat(string saveKey, float value);
        bool TryLoadFloat(string saveKey, out float value);
        float LoadFloat(string saveKey, float defaultValue);
        
        void SaveString(string saveKey, string value);
        bool TryLoadString(string saveKey, out string value);
        string LoadString(string saveKey, string defaultValue);
        
        void SaveBool(string saveKey, bool value);
        bool TryLoadBool(string saveKey, out bool value);
        bool LoadBool(string saveKey, bool defaultValue);
        
        void CleanAllData();
    }
}