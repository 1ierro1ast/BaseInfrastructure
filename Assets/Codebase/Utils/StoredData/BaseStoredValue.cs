using System;
using Codebase.Extensions;
using UnityEngine;

namespace Codebase.Utils.StoredData
{
    public abstract class BaseStoredValue<TValue>
    {
        private bool _isLoaded;
        protected TValue CurrentValue;
        protected readonly string SaveKey;
        public event Action<TValue> ValueChanged;

        public TValue Value
        {
            get
            {
                if (_isLoaded) return CurrentValue;
                Load();
                _isLoaded = true;

                return CurrentValue;
            }
            set
            {
                CurrentValue = value;
                InvokeValueChanged(CurrentValue);
                Save();
            }
        }

        public BaseStoredValue(string saveKey, TValue defaultValue)
        {
            SaveKey = saveKey;
            Value = defaultValue;
        }

        protected abstract void Save();
        protected abstract void Load();

        protected virtual void InvokeValueChanged(TValue obj)
        {
            ValueChanged?.Invoke(obj);
        }
    }

    public class StoredInt : BaseStoredValue<int>
    {
        public StoredInt(string saveKey, int defaultValue) : base(saveKey, defaultValue)
        {
        }

        protected override void Save()
        {
            PlayerPrefs.SetInt(SaveKey, CurrentValue);
        }

        protected override void Load()
        {
            CurrentValue = PlayerPrefs.GetInt(SaveKey, CurrentValue);
        }
    }

    public class StoredFloat : BaseStoredValue<float>
    {
        public StoredFloat(string saveKey, float defaultValue) : base(saveKey, defaultValue)
        {
        }

        protected override void Save()
        {
            PlayerPrefs.SetFloat(SaveKey, CurrentValue);
        }

        protected override void Load()
        {
            CurrentValue = PlayerPrefs.GetFloat(SaveKey, CurrentValue);
        }
    }

    public class StoredDouble : BaseStoredValue<double>
    {
        public StoredDouble(string saveKey, double defaultValue) : base(saveKey, defaultValue)
        {
        }

        protected override void Save()
        {
            PlayerPrefs.SetString(SaveKey, CurrentValue.ToString());
        }

        protected override void Load()
        {
            CurrentValue = double.Parse(PlayerPrefs.GetString(SaveKey, CurrentValue.ToString()));
        }
    }

    public class StoredString : BaseStoredValue<string>
    {
        public StoredString(string saveKey, string defaultValue) : base(saveKey, defaultValue)
        {
        }

        protected override void Save()
        {
            PlayerPrefs.SetString(SaveKey, CurrentValue);
        }

        protected override void Load()
        {
            CurrentValue = PlayerPrefs.GetString(SaveKey, CurrentValue);
        }
    }

    public class StoredVector3 : BaseStoredValue<Vector3>
    {
        public StoredVector3(string saveKey, Vector3 defaultValue) : base(saveKey, defaultValue)
        {
        }

        protected override void Save()
        {
            PlayerPrefs.SetString(SaveKey, CurrentValue.ToString());
        }

        protected override void Load()
        {
            CurrentValue = PlayerPrefs.GetString(SaveKey, CurrentValue.ToString()).TryConvertToVector3();
        }
    }

    public class StoredVector2 : BaseStoredValue<Vector2>
    {
        public StoredVector2(string saveKey, Vector2 defaultValue) : base(saveKey, defaultValue)
        {
        }

        protected override void Save()
        {
            PlayerPrefs.SetString(SaveKey, CurrentValue.ToString());
        }

        protected override void Load()
        {
            CurrentValue = PlayerPrefs.GetString(SaveKey, CurrentValue.ToString()).TryConvertToVector2();
        }
    }

    public class StoredChar : BaseStoredValue<char>
    {
        public StoredChar(string saveKey, char defaultValue) : base(saveKey, defaultValue)
        {
        }

        protected override void Save()
        {
            PlayerPrefs.SetString(SaveKey, CurrentValue.ToString());
        }

        protected override void Load()
        {
            CurrentValue = PlayerPrefs.GetString(SaveKey, CurrentValue.ToString()).ToCharArray()[0];
        }
    }

    public class StoredBool : BaseStoredValue<bool>
    {
        public StoredBool(string saveKey, bool defaultValue) : base(saveKey, defaultValue)
        {
        }

        protected override void Save()
        {
            PlayerPrefs.SetInt(SaveKey, CurrentValue ? 1 : 0);
        }

        protected override void Load()
        {
            CurrentValue = PlayerPrefs.GetInt(SaveKey, CurrentValue ? 1 : 0) == 1;
        }
    }
}