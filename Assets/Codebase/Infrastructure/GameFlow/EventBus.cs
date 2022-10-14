using System;

namespace Codebase.Infrastructure.GameFlow
{
    public class EventBus : IEventBus
    {
        public event Action LevelLoadedEvent;
        public event Action GamePlayStartEvent;
        public event Action LevelFinishedEvent;
        
        public event Action PlayerWinEvent;
        public event Action PlayerLoseEvent;

        public void BroadcastLevelLoaded()
        {
            LevelLoadedEvent?.Invoke();
        }

        public void BroadcastGamePlayStart()
        {
            GamePlayStartEvent?.Invoke();
        }

        public void BroadcastLevelFinished()
        {
            LevelFinishedEvent?.Invoke();
        }

        public void BroadcastPlayerWin()
        {
            PlayerWinEvent?.Invoke();
        }

        public void BroadcastPlayerLose()
        {
           PlayerLoseEvent?.Invoke();
        }
    }
}