using Codebase.Core.UI;
using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using Zenject;

namespace Codebase.Infrastructure.GameFlow
{
    public class GameStateMachine : BaseStateMachine
    {
        private DiContainer _container;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            ICoroutineRunner coroutineRunner, IGameVariables gameVariables, IUiFactory uiFactory, IEventBus eventBus,
            ILevelFactory levelFactory, ITemporaryLevelVariables temporaryLevelVariables)
        {
            RegisterState<BootstrapState>(new BootstrapState(this, sceneLoader, coroutineRunner));
            RegisterState<GameReadyState>(new GameReadyState(this, uiFactory, temporaryLevelVariables, eventBus,
                loadingCurtain, coroutineRunner));
            RegisterState<LoadLevelState>(new LoadLevelState(this, sceneLoader, loadingCurtain, levelFactory,
                gameVariables));
            RegisterState<GameplayState>(new GameplayState(this, eventBus, uiFactory, temporaryLevelVariables,
                coroutineRunner, loadingCurtain));
            RegisterState<WinState>(new WinState(this, uiFactory, gameVariables, loadingCurtain, coroutineRunner));
            RegisterState<LoseState>(new LoseState(this, uiFactory, loadingCurtain, coroutineRunner));
            
            Enter<BootstrapState>();
        }
    }
}