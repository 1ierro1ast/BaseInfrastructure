using System;
using Codebase.Infrastructure.SaveLoad;

namespace Codebase.Infrastructure.DataStorage
{
    public class GameVariables : IGameVariables
    {
        private readonly ISaveLoadService _saveLoadService;

        public event Action<int> ChangeLevelNumberEvent;
        public event Action<int> ChangeCoinsCountEvent;
        public event Action<int> ChangeSkinIdEvent;

        private const string LevelNumberSaveKey = "LevelNumber";
        private const string SkinIdSaveKey = "SkinId";
        private const string SkinProgressSaveKey = "SkinProgress";
        private const string CoinsCountSaveKey = "CoinsCount";

        private const int DefaultLevelNumber = 1;
        private const int DefaultSkinId = 0;
        private const int DefaultSkinProgress = 0;
        private const int DefaultCoinsCount = 0;

        private int _levelNumber;
        private int _coinsCount;
        private int _skinId;
        private int _skinProgress;

        public int LevelNumber
        {
            get => _levelNumber;
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                if (_levelNumber == value)
                    return;
                
                _levelNumber = value;
                SaveLevelNumber();
                
                ChangeLevelNumberEvent?.Invoke(LevelNumber);
            }
        }
        public int CoinsCount
        {
            get => _coinsCount;
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                if (_coinsCount == value)
                    return;
                
                _coinsCount = value;
                SaveCoinsCount();
                
                ChangeCoinsCountEvent?.Invoke(CoinsCount);
            }
        }

        public int SkinId
        {
            get => _skinId;
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                if (_skinId == value)
                    return;
                
                _skinId = value;
                SaveSkinId();
                
                ChangeSkinIdEvent?.Invoke(_skinId);
            }
        }

        public int SkinProgress
        {
            get => _skinProgress;
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                if (_skinProgress == value)
                    return;
                
                _skinProgress = value;
                SaveSkinId();
            }
        }

        public void SaveSkinProgress()
        {
            _saveLoadService.SaveInt(SkinProgressSaveKey, _skinId);
        }

        public void LoadSkinId()
        {
            _skinId = _saveLoadService.LoadInt(SkinIdSaveKey, DefaultSkinId);
        }

        public void SaveSkinId()
        {
            _saveLoadService.SaveInt(SkinIdSaveKey, _skinId);
        }

        public GameVariables(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;

            LoadLevelNumber();
            LoadCoinsCount();
            LoadSkinId();
            LoadSkinProgress();
        }

        public void IterateSkinId(int border)
        {
            var tempSkinId = _skinId+1;
            if (tempSkinId >= border) tempSkinId = 0;
            SkinId = tempSkinId;
        }

        public void IterateLevelNumber()
        {
            LevelNumber++;
        }

        public void AddSkinProgress(int value)
        {
            SkinProgress += value;
        }

        public void AddCoins(int value)
        {
            CoinsCount += value;
        }
        
        public bool TrySpendCoins(int value)
        {
            if (value > CoinsCount) 
                return false;
            
            CoinsCount -= value;
            return true;
        }

        public void LoadLevelNumber()
        {
            _levelNumber = _saveLoadService.LoadInt(LevelNumberSaveKey, DefaultLevelNumber);
        }

        public void SaveLevelNumber()
        {
            _saveLoadService.SaveInt(LevelNumberSaveKey, _levelNumber);
        }

        public void LoadSkinProgress()
        {
            _skinProgress = _saveLoadService.LoadInt(SkinProgressSaveKey, DefaultSkinProgress);
        }

        public void LoadCoinsCount()
        {
            _coinsCount = _saveLoadService.LoadInt(CoinsCountSaveKey, DefaultCoinsCount);
        }

        public void SaveCoinsCount()
        {
            _saveLoadService.SaveInt(CoinsCountSaveKey, _coinsCount);
        }

        public void ClearProgress()
        {
            SkinProgress = 0;
        }
    }
}