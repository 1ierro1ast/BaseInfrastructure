using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class BootstrapState : IState
    {
        private const string NextSceneName = "MainScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, 
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
            EnterLoadLevel();
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(NextSceneName);
        }

    }
}