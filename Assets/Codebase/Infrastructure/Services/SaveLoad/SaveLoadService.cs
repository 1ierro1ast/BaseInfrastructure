using UnityEngine;

namespace Codebase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        public void SaveInt(string saveKey, int value)
        {
            PlayerPrefs.SetInt(saveKey, value);
            PlayerPrefs.Save();
        }

        public bool TryLoadInt(string saveKey, out int value)
        {
            if (PlayerPrefs.HasKey(saveKey))
            {
                value = PlayerPrefs.GetInt(saveKey);
                return true;
            }

            value = 0;
            return false;
        }

        public int LoadInt(string saveKey, int defaultValue)
        {
            return PlayerPrefs.GetInt(saveKey, defaultValue);
        }


        public void SaveFloat(string saveKey, float value)
        {
            PlayerPrefs.SetFloat(saveKey, value);
            PlayerPrefs.Save();
        }

        public bool TryLoadFloat(string saveKey, out float value)
        {
            if (PlayerPrefs.HasKey(saveKey))
            {
                value = PlayerPrefs.GetFloat(saveKey);
                return true;
            }

            value = 0f;
            return false;
        }

        public float LoadFloat(string saveKey, float defaultValue)
        {
            return PlayerPrefs.GetFloat(saveKey, defaultValue);
        }


        public void SaveString(string saveKey, string value)
        {
            PlayerPrefs.SetString(saveKey, value);
            PlayerPrefs.Save();
        }

        public bool TryLoadString(string saveKey, out string value)
        {
            if (PlayerPrefs.HasKey(saveKey))
            {
                value = PlayerPrefs.GetString(saveKey);
                return true;
            }

            value = "";
            return false;
        }

        public string LoadString(string saveKey, string defaultValue)
        {
            return PlayerPrefs.GetString(saveKey, defaultValue);
        }


        public void SaveBool(string saveKey, bool value)
        {
            var temp = value ? 1 : 0;

            PlayerPrefs.SetInt(saveKey, temp);
            PlayerPrefs.Save();
        }

        public bool TryLoadBool(string saveKey, out bool value)
        {
            if (!PlayerPrefs.HasKey(saveKey))
            {
                value = false;
                return false;
            }

            var temp = PlayerPrefs.GetInt(saveKey);
            value = (temp == 1);
            return true;
        }

        public bool LoadBool(string saveKey, bool defaultValue)
        {
            var defaultTemp = defaultValue ? 1 : 0;
            var temp = PlayerPrefs.GetInt(saveKey, defaultTemp);
            return (temp == 1);
        }

        public void CleanAllData()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("DELETE ALL");
        }
    }
}