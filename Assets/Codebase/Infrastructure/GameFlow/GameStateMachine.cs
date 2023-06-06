using System;
using System.Collections.Generic;
using Codebase.Core.UI;
using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow
{
    public class GameStateMachine : BaseStateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services,
            ICoroutineRunner coroutineRunner)
        {
            RegisterState<BootstrapState>(new BootstrapState(this, sceneLoader, services, coroutineRunner));
            RegisterState<GameReadyState>(new GameReadyState(this, services.Single<IUiFactory>(),
                services.Single<ITemporaryLevelVariables>(), services.Single<IEventBus>(), loadingCurtain,
                coroutineRunner));
            RegisterState<LoadLevelState>(new LoadLevelState(this, sceneLoader, loadingCurtain,
                services.Single<ILevelFactory>(), services.Single<IGameVariables>()));
            RegisterState<GameplayState>(new GameplayState(this, services.Single<IEventBus>(),
                services.Single<IUiFactory>(), services.Single<ITemporaryLevelVariables>(), coroutineRunner,
                loadingCurtain));
            RegisterState<WinState>(new WinState(this, services.Single<IUiFactory>(),
                services.Single<IGameVariables>(), loadingCurtain, coroutineRunner));
            RegisterState<LoseState>(new LoseState(this, services.Single<IUiFactory>(), loadingCurtain,
                coroutineRunner));
        }
    }
}