using System;
using Codebase.Infrastructure.Services.SaveLoad;

namespace Codebase.Infrastructure.Services.DataStorage
{
    public class GameVariables : IGameVariables
    {
        private readonly IGetSetPrefsService _saveLoadService;

        public event Action<int> ChangeLevelNumberEvent;
        public event Action<int> ChangeCoinsCountEvent;

        private const string LevelNumberSaveKey = "LevelNumber";
        private const string CoinsCountSaveKey = "CoinsCount";

        private const int DefaultLevelNumber = 1;
        private const int DefaultCoinsCount = 0;

        private int _levelNumber;
        private int _coinsCount;

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

        public GameVariables(IGetSetPrefsService saveLoadService)
        {
            _saveLoadService = saveLoadService;

            LoadLevelNumber();
            LoadCoinsCount();
        }

        public void IterateLevelNumber()
        {
            LevelNumber++;
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
            _levelNumber = _saveLoadService.GetInt(LevelNumberSaveKey, DefaultLevelNumber);
        }

        public void SaveLevelNumber()
        {
            _saveLoadService.SetInt(LevelNumberSaveKey, _levelNumber);
        }

        public void LoadCoinsCount()
        {
            _coinsCount = _saveLoadService.GetInt(CoinsCountSaveKey, DefaultCoinsCount);
        }

        public void SaveCoinsCount()
        {
            _saveLoadService.SetInt(CoinsCountSaveKey, _coinsCount);
        }

        public void ClearProgress()
        {
            LevelNumber = 0;
            CoinsCount = 0;
        }
    }
}