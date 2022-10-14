using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.GameFlow
{
    public interface IEventBus : IService
    {
        event Action LevelLoadedEvent;
        event Action GamePlayStartEvent;
        event Action LevelFinishedEvent;
        event Action PlayerWinEvent;
        event Action PlayerLoseEvent;

        void BroadcastLevelLoaded();
        void BroadcastGamePlayStart();
        void BroadcastLevelFinished();
        void BroadcastPlayerWin();
        void BroadcastPlayerLose();
    }
}