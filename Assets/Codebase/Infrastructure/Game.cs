using Codebase.Core.UI;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain,
            AllServices services)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, services,
                coroutineRunner);
        }
    }
}