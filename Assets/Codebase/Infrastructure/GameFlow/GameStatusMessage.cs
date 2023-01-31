namespace Codebase.Infrastructure.GameFlow
{
    public enum CompleteMessage
    { Win, Lose }

    public class GameStatusMessage
    {
        public LevelStatusMessage Message { get; }

        public GameStatusMessage(LevelStatusMessage message)
        {
            Message = message;
        }
    }
}