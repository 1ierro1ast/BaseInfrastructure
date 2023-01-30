using System;

namespace Codebase.Infrastructure.GameFlow
{
    public class EventBus : IEventBus
    {
        public event Action LevelLoadedEvent;
        public event Action GamePlayStartEvent;
        public event Action LevelFinishedEvent;
        
        public event Action OnPlayerWinEvent;
        public event Action OnPlayerLoseEvent;

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
            OnPlayerWinEvent?.Invoke();
        }

        public void BroadcastPlayerLose()
        {
           OnPlayerLoseEvent?.Invoke();
        }
    }
}