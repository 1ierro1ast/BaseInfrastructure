namespace Codebase.Infrastructure.GameFlow
{
    public enum CompleteMessage
    { Win, Lose }

    public class GameCompleteMessage
    {
        public CompleteMessage Message { get; }

        public GameCompleteMessage(CompleteMessage message)
        {
            Message = message;
        }
    }
}