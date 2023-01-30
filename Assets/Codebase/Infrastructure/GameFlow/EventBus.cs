using System;
using UniRx;

namespace Codebase.Infrastructure.GameFlow
{
    public class EventBus : IEventBus
    {

        public event Action LevelFinishedEvent;

        public event Action OnPlayerWinEvent;

        public event Action OnPlayerLoseEvent;


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

    public enum LevelMessage
    { Loaded, Started, Finished };

    public enum GameStatusMessage
    { Win, Lose }

    public abstract class GameMessageBase
    {
    }

    public class GameLevelMessage
    {
        public LevelMessage Message { get; }

        public GameLevelMessage(LevelMessage message)
        {
            Message = message;
        }
    }

    public class MessageHandleService
    {
        public void SendMessage(LevelMessage message)
        {
            MessageBroker.Default
                .Publish(new GameLevelMessage(message));
        }
    }
}