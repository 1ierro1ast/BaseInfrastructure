using UnityEngine;

namespace Codebase.Infrastructure.Services.SaveLoad
{
    public class GetSetPrefsService : IGetSetPrefsService
    {
        public void SetInt(string saveKey, int value) =>
            PlayerPrefs.SetInt(saveKey, value);

        public void SetFloat(string saveKey, float value) =>
            PlayerPrefs.SetFloat(saveKey, value);

        public void SetString(string saveKey, string value) =>
            PlayerPrefs.SetString(saveKey, value);

        public void SetBool(string saveKey, bool value) =>
            PlayerPrefs.SetInt(saveKey, value ? 1 : 0);

        public int GetInt(string saveKey, int defaultValue) =>
            PlayerPrefs.GetInt(saveKey, defaultValue);

        public float GetFloat(string saveKey, float defaultValue) =>
            PlayerPrefs.GetFloat(saveKey, defaultValue);

        public string GetString(string saveKey, string defaultValue) =>
            PlayerPrefs.GetString(saveKey, defaultValue);

        public bool GetBool(string saveKey, bool defaultValue) =>
            PlayerPrefs.GetInt(saveKey, defaultValue ? 1 : 0) == 1;

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

        public bool TryGetFloat(string saveKey, out float value)
        {
            if (PlayerPrefs.HasKey(saveKey))
            {
                value = PlayerPrefs.GetFloat(saveKey);
                return true;
            }

            value = 0f;
            return false;
        }

        public bool TryGetString(string saveKey, out string value)
        {
            if (PlayerPrefs.HasKey(saveKey))
            {
                value = PlayerPrefs.GetString(saveKey);
                return true;
            }

            value = "";
            return false;
        }

        public bool TryGetBool(string saveKey, out bool value)
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

        public void CleanAllData()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("DELETE ALL");
        }
    }
}