using System;

namespace Codebase.Infrastructure.Services.DataStorage
{
    public interface IGameVariables : IService
    {
        event Action<int> ChangeLevelNumberEvent;
        event Action<int> ChangeCoinsCountEvent;
        
        int LevelNumber { get; }
        int CoinsCount { get; }
        
        void IterateLevelNumber();
        void AddCoins(int value);
        bool TrySpendCoins(int value);

        void LoadLevelNumber();
        void SaveLevelNumber();
        void LoadCoinsCount();
        void SaveCoinsCount();
        void ClearProgress();
    }
}