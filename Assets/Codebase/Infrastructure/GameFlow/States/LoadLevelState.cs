using Codebase.Core.Ads;
using Codebase.Core.UI;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ILevelFactory _levelFactory;
        private readonly IGameVariables _gameVariables;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            ILevelFactory levelFactory, IGameVariables gameVariables)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _levelFactory = levelFactory;
            _gameVariables = gameVariables;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.OpenPopup();
            _sceneLoader.LoadScene(sceneName, false, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _levelFactory.CreateLevel(_gameVariables.LevelNumber);
            _gameStateMachine.Enter<GameReadyState>();
        }
    }
}