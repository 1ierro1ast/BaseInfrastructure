using Codebase.Infrastructure.Services;
using System;

namespace Codebase.Infrastructure.GameFlow
{
    public interface IEventBus : IService
    {

        event Action LevelFinishedEvent;

        event Action OnPlayerWinEvent;

        event Action OnPlayerLoseEvent;


        void BroadcastLevelFinished();

        void BroadcastPlayerWin();

        void BroadcastPlayerLose();
    }
}