using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.DataStorage
{
    public interface IGameVariables : IService
    {
        event Action<int> ChangeLevelNumberEvent;
        event Action<int> ChangeCoinsCountEvent;
        event Action<int> ChangeSkinIdEvent;
        
        int LevelNumber { get; }
        int CoinsCount { get; }
        int SkinId { get; }
        int SkinProgress { get; }

        void IterateSkinId(int border);
        void IterateLevelNumber();
        void AddSkinProgress(int value);
        void AddCoins(int value);
        bool TrySpendCoins(int value);

        void LoadLevelNumber();
        void SaveLevelNumber();
        void LoadSkinProgress();
        void SaveSkinProgress();
        void LoadSkinId();
        void SaveSkinId();
        void LoadCoinsCount();
        void SaveCoinsCount();
        void ClearProgress();
    }
}