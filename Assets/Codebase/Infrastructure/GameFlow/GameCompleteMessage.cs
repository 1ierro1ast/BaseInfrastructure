namespace Codebase.Infrastructure.GameFlow
{
    public enum LevelStatusMessage
    { Loaded, Started, Finished };

    public class GameCompleteMessage
    {
        public CompleteMessage Message { get; }

        public GameCompleteMessage(CompleteMessage message)
        {
            Message = message;
        }
    }
}